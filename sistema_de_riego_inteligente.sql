-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 13-07-2025 a las 06:27:31
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `sistema_de_riego_inteligente`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `ActivarRiegoManual` (IN `p_zona_id` INT, IN `p_duracion_minutos` INT, IN `p_usuario` VARCHAR(50))   BEGIN
    -- Abrimos la válvula y ponemos el modo en manual
    UPDATE actuadores
    SET estado_actual = 'abierto', modo_operacion = 'manual', ultima_activacion = NOW()
    WHERE zona_id = p_zona_id;
    
    -- Guardamos en el historial que regamos
    INSERT INTO historial_riego (zona_id, fecha_hora_inicio, duracion_real, motivo_activacion, usuario_modificacion)
    VALUES (p_zona_id, NOW(), p_duracion_minutos, 'manual', p_usuario);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `CrearUsuarioSeguro` (IN `p_nombre` VARCHAR(50), IN `p_rol` ENUM('admin','operador','tecnico'), IN `p_pass_plain` VARCHAR(128), IN `p_email` VARCHAR(100))   BEGIN
  DECLARE v_salt VARBINARY(16)
          DEFAULT UNHEX(SUBSTRING(REPLACE(UUID(),'-',''),1,32));
  DECLARE v_hash VARBINARY(64);

  SET v_hash = SHA2(CONCAT(v_salt, p_pass_plain), 256);

  INSERT INTO usuarios(nombre_usuario, rol, salt, hash, email)
  VALUES (p_nombre, p_rol, v_salt, v_hash, p_email);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtenerAlertasPendientes` ()   BEGIN
    SELECT a.alerta_id, a.tipo_alerta, a.mensaje, a.fecha_hora, z.nombre_zona
    FROM alertas a
    JOIN zonas_riego z ON a.zona_id = z.zona_id
    WHERE a.estado = 'pendiente'
    ORDER BY a.fecha_hora DESC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `RegistrarLecturaSensor` (IN `p_sensor_id` INT, IN `p_valor` DECIMAL(10,2))   BEGIN
    -- Actualizamos el valor y la fecha del sensor
    UPDATE sensores
    SET valor_actual = p_valor, fecha_lectura = NOW()
    WHERE sensor_id = p_sensor_id;
    
    -- Si es un sensor de humedad y el valor está raro (muy bajo o muy alto), lanzamos una alerta
    IF (SELECT tipo_sensor FROM sensores WHERE sensor_id = p_sensor_id) = 'humedad_suelo_YL69'
       AND (p_valor < 20.00 OR p_valor > 80.00) THEN
        INSERT INTO alertas (zona_id, tipo_alerta, mensaje, fecha_hora, estado)
        SELECT zona_id, 'rango_humedad', 
               CONCAT('¡Ojo! La humedad está fuera de rango: ', p_valor, '%'),
               NOW(), 'pendiente'
        FROM sensores WHERE sensor_id = p_sensor_id;
    END IF;
END$$

--
-- Funciones
--
CREATE DEFINER=`root`@`localhost` FUNCTION `ClaveActiva` () RETURNS VARBINARY(64) READS SQL DATA BEGIN
  RETURN (
    SELECT clave
      FROM claves_maestras
     WHERE activa = 1
     LIMIT 1
  );
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `VerificarPassword` (`p_user` VARCHAR(50), `p_pass` VARCHAR(128)) RETURNS TINYINT(1) READS SQL DATA BEGIN
  DECLARE v_s VARBINARY(16);
  DECLARE v_h VARBINARY(64);

  SELECT salt, hash INTO v_s, v_h
    FROM usuarios
   WHERE nombre_usuario = p_user
   LIMIT 1;

  IF v_s IS NULL THEN RETURN 0; END IF;
  RETURN v_h = SHA2(CONCAT(v_s, p_pass), 256);
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `actuadores`
--

CREATE TABLE `actuadores` (
  `actuador_id` int(11) NOT NULL,
  `zona_id` int(11) NOT NULL,
  `estado_actual` enum('abierto','cerrado') DEFAULT 'cerrado',
  `modo_operacion` enum('manual','automatico') DEFAULT 'automatico',
  `ultima_activacion` datetime DEFAULT NULL,
  `pin_arduino` int(11) NOT NULL,
  `ultima_mantenimiento` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `actuadores`
--

INSERT INTO `actuadores` (`actuador_id`, `zona_id`, `estado_actual`, `modo_operacion`, `ultima_activacion`, `pin_arduino`, `ultima_mantenimiento`) VALUES
(10, 1, 'abierto', 'manual', '2025-07-12 22:59:47', 7, '2025-06-10 08:00:00'),
(11, 2, 'abierto', 'manual', '2025-07-12 23:04:28', 8, '2025-06-11 09:00:00'),
(12, 3, 'abierto', 'manual', '2025-07-12 23:15:22', 9, '2025-06-12 06:30:00');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `alertas`
--

CREATE TABLE `alertas` (
  `alerta_id` int(11) NOT NULL,
  `zona_id` int(11) NOT NULL,
  `tipo_alerta` varchar(50) NOT NULL,
  `mensaje` varbinary(1024) DEFAULT NULL,
  `fecha_hora` datetime NOT NULL,
  `estado` enum('pendiente','resuelta','en_proceso') DEFAULT 'pendiente',
  `usuario_asignado` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `alertas`
--

INSERT INTO `alertas` (`alerta_id`, `zona_id`, `tipo_alerta`, `mensaje`, `fecha_hora`, `estado`, `usuario_asignado`) VALUES
(1, 1, 'sensor_falla', 0x53656e736f722064652068756d6564616420594c2d3639206e6f20726573706f6e646520636f7272656374616d656e7465, '2025-06-13 01:17:00', 'pendiente', 'mateo_cevallos'),
(2, 2, 'rango_temperatura', 0x54656d706572617475726120656c65766164612064657465637461646120706f72204448543131, '2025-06-13 01:17:00', 'en_proceso', 'bryan_medina'),
(3, 3, 'lluvia_detectada', 0x53656e736f722072657369737469766f20646574656374c3b3206c6c75766961, '2025-06-13 01:17:00', 'pendiente', 'jhoe_cadena_admin'),
(0, 1, 'prueba_cifrada', 0x784993df88ad39880a3572d501d1be1b63b5963871c875dd18c8fbe28a03d463, '2025-06-27 21:39:11', 'pendiente', NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `auditoria_usuarios`
--

CREATE TABLE `auditoria_usuarios` (
  `id_auditoria` int(11) NOT NULL,
  `accion` varchar(10) DEFAULT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  `nombre_usuario` varchar(50) DEFAULT NULL,
  `rol` varchar(20) DEFAULT NULL,
  `email` varbinary(255) DEFAULT NULL,
  `fecha_cambio` datetime DEFAULT current_timestamp(),
  `usuario_responsable` varchar(50) DEFAULT current_user()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `auditoria_usuarios`
--

INSERT INTO `auditoria_usuarios` (`id_auditoria`, `accion`, `usuario_id`, `nombre_usuario`, `rol`, `email`, `fecha_cambio`, `usuario_responsable`) VALUES
(1, 'INSERT', 0, 'karla_montero', 'tecnico', 0x6b61726c6140666c6f726973686f702e636f6d, '2025-07-09 17:41:32', 'root@localhost'),
(2, 'INSERT', 7, 'usuario_test', 'admin', 0x7573756172696f40666c6f726973686f702e636f6d, '2025-07-10 19:50:15', 'root@localhost'),
(3, 'INSERT', 8, 'admin_test', 'admin', 0x61646d696e40666c6f726973686f702e636f6d, '2025-07-12 22:06:27', 'root@localhost');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `claves_maestras`
--

CREATE TABLE `claves_maestras` (
  `clave_id` tinyint(4) NOT NULL,
  `nombre` varchar(50) DEFAULT NULL,
  `clave` varbinary(64) DEFAULT NULL,
  `fecha_alta` datetime DEFAULT current_timestamp(),
  `activa` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `claves_maestras`
--

INSERT INTO `claves_maestras` (`clave_id`, `nombre`, `clave`, `fecha_alta`, `activa`) VALUES
(1, 'K1_AES256', 0x00112233445566778899aabbccddeeff00112233445566778899aabbccddeeff, '2025-06-27 19:48:52', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `configuracion_hardware`
--

CREATE TABLE `configuracion_hardware` (
  `config_id` int(11) NOT NULL,
  `parametro` varchar(50) NOT NULL,
  `valor` varchar(50) NOT NULL,
  `descripcion` text DEFAULT NULL,
  `zona_id` int(11) DEFAULT NULL,
  `fecha_configuracion` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `configuracion_hardware`
--

INSERT INTO `configuracion_hardware` (`config_id`, `parametro`, `valor`, `descripcion`, `zona_id`, `fecha_configuracion`) VALUES
(1, 'intervalo_lectura', '300', 'Intervalo en segundos para sensores', 1, '2025-06-13 01:17:00'),
(2, 'pin_sensor_humedad', 'A0', 'Pin para sensor de humedad', 2, '2025-06-13 01:17:00');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `datos_meteorologicos`
--

CREATE TABLE `datos_meteorologicos` (
  `registro_clima_id` int(11) NOT NULL,
  `fecha_hora` datetime NOT NULL,
  `temperatura` decimal(5,2) DEFAULT NULL,
  `humedad_ambiente` decimal(5,2) DEFAULT NULL,
  `precipitacion` decimal(5,2) DEFAULT NULL,
  `zona_id` int(11) NOT NULL,
  `fuente_dato` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `datos_meteorologicos`
--

INSERT INTO `datos_meteorologicos` (`registro_clima_id`, `fecha_hora`, `temperatura`, `humedad_ambiente`, `precipitacion`, `zona_id`, `fuente_dato`) VALUES
(1, '2025-06-13 01:17:00', 22.50, 70.00, 0.00, 1, 'DHT11'),
(2, '2025-06-13 01:17:00', 21.80, 68.00, 0.20, 2, 'API_OpenWeather'),
(3, '2025-06-13 01:17:00', 23.00, 65.00, 0.00, 3, 'DHT11');

--
-- Disparadores `datos_meteorologicos`
--
DELIMITER $$
CREATE TRIGGER `datos_meteorologicos` AFTER INSERT ON `datos_meteorologicos` FOR EACH ROW BEGIN
    IF NEW.precipitacion > 5.00 THEN
        INSERT INTO alertas (zona_id, tipo_alerta, mensaje, fecha_hora, estado)
        VALUES (NEW.zona_id, 'precipitacion_alta',
                CONCAT('¡Alerta! Lluvia fuerte detectada: ', NEW.precipitacion, ' mm'),
                NOW(), 'pendiente');
    END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `historial_riego`
--

CREATE TABLE `historial_riego` (
  `registro_id` int(11) NOT NULL,
  `zona_id` int(11) NOT NULL,
  `fecha_hora_inicio` datetime NOT NULL,
  `fecha_hora_fin` datetime DEFAULT NULL,
  `duracion_real` int(11) DEFAULT NULL,
  `motivo_activacion` enum('programado','manual','automatico_por_sensor') NOT NULL,
  `usuario_modificacion` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `historial_riego`
--

INSERT INTO `historial_riego` (`registro_id`, `zona_id`, `fecha_hora_inicio`, `fecha_hora_fin`, `duracion_real`, `motivo_activacion`, `usuario_modificacion`) VALUES
(1, 1, '2025-06-13 01:17:00', '2025-06-13 01:27:00', 10, 'programado', 'bryan_medina_root'),
(2, 2, '2025-06-13 01:10:00', '2025-06-13 01:20:00', 10, 'automatico_por_sensor', 'mateo_cevallos');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `log_conexiones`
--

CREATE TABLE `log_conexiones` (
  `id_log` int(11) NOT NULL,
  `usuario_id` int(11) NOT NULL,
  `ip` varchar(45) NOT NULL,
  `fecha_conexion` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `log_conexiones`
--

INSERT INTO `log_conexiones` (`id_log`, `usuario_id`, `ip`, `fecha_conexion`) VALUES
(1, 6, '192.168.1.20', '2025-07-11 14:49:19');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `programacion_riego`
--

CREATE TABLE `programacion_riego` (
  `programacion_id` int(11) NOT NULL,
  `zona_id` int(11) NOT NULL,
  `frecuencia` varchar(50) NOT NULL,
  `hora_inicio` time NOT NULL,
  `duracion_minutos` int(11) NOT NULL,
  `estado` enum('activo','inactivo','pendiente') DEFAULT 'activo',
  `ultima_modificacion` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `programacion_riego`
--

INSERT INTO `programacion_riego` (`programacion_id`, `zona_id`, `frecuencia`, `hora_inicio`, `duracion_minutos`, `estado`, `ultima_modificacion`) VALUES
(1, 1, 'diaria', '05:00:00', 10, 'activo', '2025-06-13 01:17:00'),
(2, 2, 'interdiaria', '06:00:00', 15, 'activo', '2025-06-13 01:17:00'),
(3, 3, 'semanal', '07:00:00', 20, 'activo', '2025-06-13 01:17:00');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `sensores`
--

CREATE TABLE `sensores` (
  `sensor_id` int(11) NOT NULL,
  `tipo_sensor` varchar(50) NOT NULL,
  `zona_id` int(11) NOT NULL,
  `valor_actual` decimal(10,2) DEFAULT NULL,
  `unidad` varchar(20) DEFAULT NULL,
  `fecha_lectura` datetime DEFAULT NULL,
  `estado` enum('activo','inactivo','mantenimiento') DEFAULT 'activo'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `sensores`
--

INSERT INTO `sensores` (`sensor_id`, `tipo_sensor`, `zona_id`, `valor_actual`, `unidad`, `fecha_lectura`, `estado`) VALUES
(19, 'humedad_suelo_YL69', 1, 32.40, '%', '2025-06-13 01:17:00', 'activo'),
(20, 'temperatura_DHT11', 1, 23.80, '°C', '2025-06-13 01:17:00', 'activo'),
(21, 'lluvia_resistivo', 1, 0.10, 'mm', '2025-06-13 01:17:00', 'activo'),
(22, 'humedad_suelo_YL69', 2, 45.00, '%', '2025-06-13 01:17:00', 'activo'),
(23, 'temperatura_DHT11', 2, 24.00, '°C', '2025-06-13 01:17:00', 'activo'),
(24, 'humedad_suelo_YL69', 3, 50.00, '%', '2025-06-13 01:17:00', 'mantenimiento'),
(25, 'temperatura_DHT11', 3, 22.00, '°C', '2025-06-13 01:17:00', 'activo');

--
-- Disparadores `sensores`
--
DELIMITER $$
CREATE TRIGGER `sensor_mantenimiento` AFTER UPDATE ON `sensores` FOR EACH ROW BEGIN
  IF NEW.estado = 'mantenimiento' AND OLD.estado <> 'mantenimiento' THEN
    INSERT INTO alertas (zona_id, tipo_alerta, mensaje, fecha_hora, estado)
    VALUES (
      NEW.zona_id,
      'sensor_mantenimiento',
      AES_ENCRYPT(
        CONCAT('¡Cuidado! El sensor ', NEW.tipo_sensor, ' está en mantenimiento'),
        ClaveActiva()   -- clave de 32 bytes
      ),
      NOW(),
      'pendiente'
    );
  END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `usuario_id` int(11) NOT NULL,
  `nombre_usuario` varchar(50) NOT NULL,
  `rol` enum('admin','operador','tecnico') DEFAULT 'operador',
  `email` varbinary(255) DEFAULT NULL,
  `ultima_conexion` datetime DEFAULT NULL,
  `salt` varbinary(16) NOT NULL,
  `hash` varbinary(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`usuario_id`, `nombre_usuario`, `rol`, `email`, `ultima_conexion`, `salt`, `hash`) VALUES
(1, 'bryan_medina_root', 'admin', 0x627279616e40656d70726573612e636f6d, '2025-06-13 01:17:00', 0xa97203d753b911f09047f4b52017c143, 0x38353837666335643765383763363838383665323030313732306638306164636137666230363435303334366339336339303239303964353063343461366165),
(2, 'mateo_cevallos', 'tecnico', 0x6d6174656f40656d70726573612e636f6d, '2025-06-13 01:17:00', 0xa9720f5953b911f09047f4b52017c143, 0x64616135393062303536383766346437383239303937643937346365383139373532643430323264393839366431616430323435623165613632396635366633),
(3, 'jhoe_cadena_admin', 'operador', 0x6a686f6540656d70726573612e636f6d, '2025-06-13 01:17:00', 0xa9720ffb53b911f09047f4b52017c143, 0x36666232373064343566333766626431653632663437313837366661653062316135326633656431613234656166643765393335383264633830316235663930),
(4, 'nuevo', 'operador', 0x6e7565766f40656d70726573612e636f6d, '2025-06-13 01:17:00', 0xa972105a53b911f09047f4b52017c143, 0x33383738623430646137636563353061336537343463343861396535633431363337666330316238326661306235643137303135393461333633643733313134),
(5, 'demo', 'operador', 0x64656d6f406d61696c2e636f6d, NULL, 0xa385dfab53bb11f09047f4b52017c143, 0x66613664376362376263333965326631626139613061383836333264363132386333653837373231643962353239663834323933353936396439633334356137),
(6, 'karla_montero', 'tecnico', 0x6b61726c6140666c6f726973686f702e636f6d, NULL, 0xdcbdcdce5d1511f0afa6f4b52017c143, 0x39336337303332363766343762663638393033303565306239383861363537303366363130636432623661646531363536623732393665626665626164643939),
(7, 'usuario_test', 'admin', 0x7573756172696f40666c6f726973686f702e636f6d, NULL, 0x02257d915df111f0a5bbf4b52017c143, 0x31646239636436373135656131653130613433393230396161626135656666323765386634653939613236636238363639643232383366353130636363326332),
(8, 'admin_test', 'admin', 0x61646d696e40666c6f726973686f702e636f6d, NULL, 0x5dcf71a95f9611f09346f4b52017c143, 0x38373331386530653235663932663835656435343362383131613661613661313237323732666434656133653662643039333062333337336266626265626131);

--
-- Disparadores `usuarios`
--
DELIMITER $$
CREATE TRIGGER `trg_aud_usuarios_delete` AFTER DELETE ON `usuarios` FOR EACH ROW BEGIN
  INSERT INTO auditoria_usuarios (
      accion, usuario_id, nombre_usuario, rol, email
  ) VALUES (
      'DELETE', OLD.usuario_id, OLD.nombre_usuario, OLD.rol, OLD.email
  );
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `trg_aud_usuarios_insert` AFTER INSERT ON `usuarios` FOR EACH ROW BEGIN
  INSERT INTO auditoria_usuarios (
      accion, usuario_id, nombre_usuario, rol, email
  ) VALUES (
      'INSERT', NEW.usuario_id, NEW.nombre_usuario, NEW.rol, NEW.email
  );
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `trg_aud_usuarios_update` AFTER UPDATE ON `usuarios` FOR EACH ROW BEGIN
  INSERT INTO auditoria_usuarios (
      accion, usuario_id, nombre_usuario, rol, email
  ) VALUES (
      'UPDATE', OLD.usuario_id, OLD.nombre_usuario, OLD.rol, OLD.email
  );
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura Stand-in para la vista `vista_auditoria_alertas`
-- (Véase abajo para la vista actual)
--
CREATE TABLE `vista_auditoria_alertas` (
`alerta_id` int(11)
,`zona_id` int(11)
,`tipo_alerta` varchar(50)
,`mensaje` varbinary(1024)
,`fecha_hora` datetime
,`estado` enum('pendiente','resuelta','en_proceso')
,`usuario_asignado` varchar(50)
);

-- --------------------------------------------------------

--
-- Estructura Stand-in para la vista `vista_config_operativa`
-- (Véase abajo para la vista actual)
--
CREATE TABLE `vista_config_operativa` (
`config_id` int(11)
,`parametro` varchar(50)
,`valor` varchar(50)
,`fecha_configuracion` datetime
);

-- --------------------------------------------------------

--
-- Estructura Stand-in para la vista `vista_historial_publico`
-- (Véase abajo para la vista actual)
--
CREATE TABLE `vista_historial_publico` (
`registro_id` int(11)
,`zona_id` int(11)
,`fecha_hora_inicio` datetime
,`duracion_real` int(11)
,`motivo_activacion` enum('programado','manual','automatico_por_sensor')
,`usuario_modificacion` varchar(50)
);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `zonas_riego`
--

CREATE TABLE `zonas_riego` (
  `zona_id` int(11) NOT NULL,
  `nombre_zona` varchar(50) NOT NULL,
  `ubicacion` varchar(100) NOT NULL,
  `tipo_cultivo` varchar(50) DEFAULT NULL,
  `area_m2` decimal(10,2) NOT NULL,
  `responsable_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `zonas_riego`
--

INSERT INTO `zonas_riego` (`zona_id`, `nombre_zona`, `ubicacion`, `tipo_cultivo`, `area_m2`, `responsable_id`) VALUES
(1, 'Invernadero Rosas', 'Sector Norte', 'Rosas', 150.00, NULL),
(2, 'Bloque Tulipanes', 'Sector Este', 'Tulipanes', 120.00, NULL),
(3, 'Zona Margaritas', 'Patio central', 'Margaritas', 90.00, NULL);

-- --------------------------------------------------------

--
-- Estructura para la vista `vista_auditoria_alertas`
--
DROP TABLE IF EXISTS `vista_auditoria_alertas`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vista_auditoria_alertas`  AS SELECT `alertas`.`alerta_id` AS `alerta_id`, `alertas`.`zona_id` AS `zona_id`, `alertas`.`tipo_alerta` AS `tipo_alerta`, `alertas`.`mensaje` AS `mensaje`, `alertas`.`fecha_hora` AS `fecha_hora`, `alertas`.`estado` AS `estado`, `alertas`.`usuario_asignado` AS `usuario_asignado` FROM `alertas` WHERE `alertas`.`estado` in ('pendiente','en_proceso') ;

-- --------------------------------------------------------

--
-- Estructura para la vista `vista_config_operativa`
--
DROP TABLE IF EXISTS `vista_config_operativa`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vista_config_operativa`  AS SELECT `configuracion_hardware`.`config_id` AS `config_id`, `configuracion_hardware`.`parametro` AS `parametro`, `configuracion_hardware`.`valor` AS `valor`, `configuracion_hardware`.`fecha_configuracion` AS `fecha_configuracion` FROM `configuracion_hardware` WHERE `configuracion_hardware`.`zona_id` is not null ;

-- --------------------------------------------------------

--
-- Estructura para la vista `vista_historial_publico`
--
DROP TABLE IF EXISTS `vista_historial_publico`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vista_historial_publico`  AS SELECT `historial_riego`.`registro_id` AS `registro_id`, `historial_riego`.`zona_id` AS `zona_id`, `historial_riego`.`fecha_hora_inicio` AS `fecha_hora_inicio`, `historial_riego`.`duracion_real` AS `duracion_real`, `historial_riego`.`motivo_activacion` AS `motivo_activacion`, `historial_riego`.`usuario_modificacion` AS `usuario_modificacion` FROM `historial_riego` WHERE `historial_riego`.`motivo_activacion` <> 'manual' ;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `auditoria_usuarios`
--
ALTER TABLE `auditoria_usuarios`
  ADD PRIMARY KEY (`id_auditoria`);

--
-- Indices de la tabla `claves_maestras`
--
ALTER TABLE `claves_maestras`
  ADD PRIMARY KEY (`clave_id`);

--
-- Indices de la tabla `historial_riego`
--
ALTER TABLE `historial_riego`
  ADD PRIMARY KEY (`registro_id`);

--
-- Indices de la tabla `log_conexiones`
--
ALTER TABLE `log_conexiones`
  ADD PRIMARY KEY (`id_log`),
  ADD KEY `usuario_id` (`usuario_id`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`usuario_id`);

--
-- Indices de la tabla `zonas_riego`
--
ALTER TABLE `zonas_riego`
  ADD PRIMARY KEY (`zona_id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `auditoria_usuarios`
--
ALTER TABLE `auditoria_usuarios`
  MODIFY `id_auditoria` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `historial_riego`
--
ALTER TABLE `historial_riego`
  MODIFY `registro_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `log_conexiones`
--
ALTER TABLE `log_conexiones`
  MODIFY `id_log` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `usuario_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `zonas_riego`
--
ALTER TABLE `zonas_riego`
  MODIFY `zona_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `log_conexiones`
--
ALTER TABLE `log_conexiones`
  ADD CONSTRAINT `log_conexiones_ibfk_1` FOREIGN KEY (`usuario_id`) REFERENCES `usuarios` (`usuario_id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
