USE [TP2]
GO
/****** Object:  Table [dbo].[alumnos_inscripciones]    Script Date: 14/09/2020 17:25:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[alumnos_inscripciones](
	[id_inscripcion] [int] IDENTITY(1,1) NOT NULL,
	[id_alumno] [int] NOT NULL,
	[id_curso] [int] NOT NULL,
	[condicion] [varchar](50) NOT NULL,
	[nota] [int] NULL,
 CONSTRAINT [PK_alumnos_inscripciones] PRIMARY KEY CLUSTERED 
(
	[id_inscripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[comisiones]    Script Date: 14/09/2020 17:25:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comisiones](
	[id_comision] [int] IDENTITY(1,1) NOT NULL,
	[desc_comision] [varchar](50) NOT NULL,
	[anio_especialidad] [int] NOT NULL,
	[id_plan] [int] NOT NULL,
 CONSTRAINT [PK_comisiones] PRIMARY KEY CLUSTERED 
(
	[id_comision] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cursos]    Script Date: 14/09/2020 17:25:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cursos](
	[id_curso] [int] IDENTITY(1,1) NOT NULL,
	[id_materia] [int] NOT NULL,
	[id_comision] [int] NOT NULL,
	[anio_calendario] [int] NOT NULL,
	[cupo] [int] NOT NULL,
 CONSTRAINT [PK_cursos] PRIMARY KEY CLUSTERED 
(
	[id_curso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[docentes_cursos]    Script Date: 14/09/2020 17:25:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[docentes_cursos](
	[id_dictado] [int] IDENTITY(1,1) NOT NULL,
	[id_curso] [int] NOT NULL,
	[id_docente] [int] NOT NULL,
	[cargo] [int] NOT NULL,
 CONSTRAINT [PK_docentes_cursos] PRIMARY KEY CLUSTERED 
(
	[id_dictado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[especialidades]    Script Date: 14/09/2020 17:25:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[especialidades](
	[id_especialidad] [int] IDENTITY(1,1) NOT NULL,
	[desc_especialidad] [varchar](50) NOT NULL,
 CONSTRAINT [PK_especialidades] PRIMARY KEY CLUSTERED 
(
	[id_especialidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[materias]    Script Date: 14/09/2020 17:25:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[materias](
	[id_materia] [int] IDENTITY(1,1) NOT NULL,
	[desc_materia] [varchar](50) NOT NULL,
	[hs_semanales] [int] NOT NULL,
	[hs_totales] [int] NOT NULL,
	[id_plan] [int] NOT NULL,
 CONSTRAINT [PK_materias] PRIMARY KEY CLUSTERED 
(
	[id_materia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[modulos]    Script Date: 14/09/2020 17:25:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[modulos](
	[id_modulo] [int] IDENTITY(1,1) NOT NULL,
	[desc_modulo] [varchar](50) NULL,
	[ejecuta] [varchar](50) NULL,
 CONSTRAINT [PK_modulos] PRIMARY KEY CLUSTERED 
(
	[id_modulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[modulos_usuarios]    Script Date: 14/09/2020 17:25:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[modulos_usuarios](
	[id_modulo_usuario] [int] IDENTITY(1,1) NOT NULL,
	[id_modulo] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[alta] [bit] NULL,
	[baja] [bit] NULL,
	[modificacion] [bit] NULL,
	[consulta] [bit] NULL,
 CONSTRAINT [PK_modulos_usuarios] PRIMARY KEY CLUSTERED 
(
	[id_modulo_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[personas]    Script Date: 14/09/2020 17:25:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[personas](
	[id_persona] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[direccion] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[telefono] [varchar](50) NULL,
	[fecha_nac] [datetime] NOT NULL,
	[legajo] [int] NOT NULL,
	[tipo_persona] [int] NOT NULL,
	[id_plan] [int] NOT NULL,
 CONSTRAINT [PK_personas] PRIMARY KEY CLUSTERED 
(
	[id_persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[planes]    Script Date: 14/09/2020 17:25:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[planes](
	[id_plan] [int] IDENTITY(1,1) NOT NULL,
	[desc_plan] [varchar](50) NOT NULL,
	[id_especialidad] [int] NOT NULL,
 CONSTRAINT [PK_planes] PRIMARY KEY CLUSTERED 
(
	[id_plan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 14/09/2020 17:25:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre_usuario] [varchar](50) NOT NULL,
	[clave] [varchar](50) NOT NULL,
	[habilitado] [bit] NOT NULL,
	[nombre] [varchar](50) NULL,
	[apellido] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[cambia_clave] [bit] NULL,
	[id_persona] [int] NOT NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[comisiones] ON 

INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (53, N'1k1', 2012, 25)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (54, N'1k1', 2013, 25)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (55, N'3k5', 2011, 25)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (56, N'5k1', 2018, 25)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (57, N'2k1', 2020, 39)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (58, N'2k2', 2017, 39)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (59, N'4k4', 2015, 39)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (60, N'1k3', 2011, 39)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (61, N'1k1', 2020, 32)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (62, N'2k2', 2018, 32)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (63, N'2k3', 2015, 32)
SET IDENTITY_INSERT [dbo].[comisiones] OFF
GO
SET IDENTITY_INSERT [dbo].[cursos] ON 

INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo]) VALUES (73, 35, 57, 2011, 30)
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo]) VALUES (74, 36, 58, 2013, 20)
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo]) VALUES (75, 40, 53, 2009, 50)
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo]) VALUES (76, 47, 63, 2013, 40)
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo]) VALUES (77, 42, 53, 2011, 10)
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo]) VALUES (78, 39, 60, 2011, 35)
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo]) VALUES (79, 45, 55, 2013, 10)
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo]) VALUES (80, 38, 60, 2011, 30)
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo]) VALUES (81, 50, 62, 2015, 20)
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo]) VALUES (82, 49, 61, 2021, 30)
SET IDENTITY_INSERT [dbo].[cursos] OFF
GO
SET IDENTITY_INSERT [dbo].[especialidades] ON 

INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (10, N'Ingeniería en Sistemas')
INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (13, N'Ingenieria Electrica')
INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (14, N'Ingenieria Electronica')
INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (15, N'Ingeniería Ambiental')
INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (16, N'Ingeniería Física')
INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (17, N'Ingeniería Química')
INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (29, N'Ingeniería Mecánica')
SET IDENTITY_INSERT [dbo].[especialidades] OFF
GO
SET IDENTITY_INSERT [dbo].[materias] ON 

INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (35, N'.NET', 4, 60, 39)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (36, N'Algebra', 6, 80, 39)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (37, N'Analisis Matematico I', 6, 70, 39)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (38, N'Diseño de Sistemas', 8, 80, 39)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (39, N'Java', 4, 60, 39)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (40, N'Arquitectura de las Computadoras', 4, 60, 25)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (41, N'Analisis Matematico I', 4, 60, 25)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (42, N'Analisis Matematico II', 8, 60, 25)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (43, N'Ingles I', 4, 50, 25)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (44, N'Ingles II', 6, 70, 25)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (45, N'Fisica I', 6, 60, 25)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (46, N'Quimica Organica', 6, 80, 32)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (47, N'Matematica A', 4, 60, 32)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (48, N'Matematica B', 5, 70, 32)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (49, N'Termodinamica', 4, 60, 32)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (50, N'Instrumentacion y Control de Procesos', 4, 60, 32)
SET IDENTITY_INSERT [dbo].[materias] OFF
GO
SET IDENTITY_INSERT [dbo].[personas] ON 

INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (17, N'Elias', N'Dinolfo', N'una calle', N'eliasdinolfo@gmail.com', N'1234565321', CAST(N'1999-07-05T00:00:00.000' AS DateTime), 45788, 2, 25)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (19, N'Jose', N'Gonzalez', N'Callao', N'jose01@gmail.com', N'3414353535', CAST(N'2001-01-05T00:00:00.000' AS DateTime), 12355, 0, 32)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (20, N'Brian', N'Stefano', N'Mexico', N'bstef@gmail.com', N'3414501899', CAST(N'1996-02-01T00:00:00.000' AS DateTime), 43532, 1, 39)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (25, N'Crisitan', N'Rivas', N'Italia', N'cris_30@gmail.com', N'3414302819', CAST(N'1993-02-05T00:00:00.000' AS DateTime), 18902, 0, 39)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (28, N'Martin', N'Yoda', N'Avellaneda', N'martYoDA@gmail.com', N'4323120532', CAST(N'1999-03-02T00:00:00.000' AS DateTime), 45012, 0, 32)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (35, N'Elias', N'Dinolfo', N'una calle', N'dadad@gmail.com', N'4444444444', CAST(N'1999-07-05T00:00:00.000' AS DateTime), 45788, 0, 25)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (36, N'Mariana', N'Hurtado', N'Bolivia', N'marhur@gmail.com', N'4444444444', CAST(N'1980-02-05T00:00:00.000' AS DateTime), 44444, 1, 25)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (37, N'Rolando', N'Schiavi', N'perez bulnes', N'rolando@gmail.com', N'4444444444', CAST(N'1995-02-02T00:00:00.000' AS DateTime), 44444, 0, 39)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (38, N'Juan Enriquez', N'Gonzalez', N'azcuenaga', N'juanenri@live.com', N'4444444444', CAST(N'1999-01-02T00:00:00.000' AS DateTime), 44443, 0, 39)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (39, N'Eric', N'Gutierrez', N'rioja', N'erigut@gmail.com', N'3423232323', CAST(N'1996-01-02T00:00:00.000' AS DateTime), 33322, 0, 39)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (40, N'Miriam', N'Crespo', N'guatemala', N'mircresp@gmail.com', N'2323232323', CAST(N'1980-01-02T00:00:00.000' AS DateTime), 22222, 1, 32)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (41, N'Micaela', N'Del Prado', N'ituzaingo', N'micadelPrado@gmail.com', N'2323112121', CAST(N'1979-02-05T00:00:00.000' AS DateTime), 32123, 1, 32)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (42, N'Romina', N'Sanchez', N'Juan José Paso', N'romsan@gmail.com', N'2323232323', CAST(N'1996-10-08T00:00:00.000' AS DateTime), 33232, 1, 25)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (43, N'SuperAdmin', N'sa', N'suipacha', N'superadmin@gmail.com', N'1234567890', CAST(N'1995-02-05T00:00:00.000' AS DateTime), 43211, 2, 25)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (44, N'Jorge', N'Bernardo', N'santa fe', N'jorgber@gmail.com', N'3232323232', CAST(N'1999-02-03T00:00:00.000' AS DateTime), 43020, 0, 32)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (45, N'Juan', N'Cristaldo', N'AV Pellegrini', N'jc@gmail.com', N'4303212003', CAST(N'1999-02-05T00:00:00.000' AS DateTime), 43212, 0, 36)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (46, N'Lucia', N'Artegui', N'Kyle', N'luarte@gmail.com', N'3414323123', CAST(N'1998-02-05T00:00:00.000' AS DateTime), 44444, 1, 36)
SET IDENTITY_INSERT [dbo].[personas] OFF
GO
SET IDENTITY_INSERT [dbo].[planes] ON 

INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (25, N'Plan2007ISI', 10)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (30, N'Plan1999FI', 16)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (32, N'Plan2010Q', 17)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (33, N'Plan2008EC', 14)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (34, N'Plan2013EL', 13)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (36, N'Plan2011AM', 15)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (38, N'Plan2011FI', 16)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (39, N'Plan2010ISI', 10)
SET IDENTITY_INSERT [dbo].[planes] OFF
GO
SET IDENTITY_INSERT [dbo].[usuarios] ON 

INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (20, N'Elias_D99', N'12345678', 1, NULL, NULL, NULL, NULL, 17)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (23, N'CrisRiva', N'12345678', 1, NULL, NULL, NULL, NULL, 25)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (25, N'bStefa23', N'12345678', 1, NULL, NULL, NULL, NULL, 20)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (29, N'marHurtado', N'12345678', 1, NULL, NULL, NULL, NULL, 36)
SET IDENTITY_INSERT [dbo].[usuarios] OFF
GO
ALTER TABLE [dbo].[alumnos_inscripciones]  WITH CHECK ADD  CONSTRAINT [FK_alumnos_inscripciones_cursos] FOREIGN KEY([id_curso])
REFERENCES [dbo].[cursos] ([id_curso])
GO
ALTER TABLE [dbo].[alumnos_inscripciones] CHECK CONSTRAINT [FK_alumnos_inscripciones_cursos]
GO
ALTER TABLE [dbo].[alumnos_inscripciones]  WITH CHECK ADD  CONSTRAINT [FK_alumnos_inscripciones_personas] FOREIGN KEY([id_alumno])
REFERENCES [dbo].[personas] ([id_persona])
GO
ALTER TABLE [dbo].[alumnos_inscripciones] CHECK CONSTRAINT [FK_alumnos_inscripciones_personas]
GO
ALTER TABLE [dbo].[comisiones]  WITH CHECK ADD  CONSTRAINT [FK_comisiones_planes] FOREIGN KEY([id_plan])
REFERENCES [dbo].[planes] ([id_plan])
GO
ALTER TABLE [dbo].[comisiones] CHECK CONSTRAINT [FK_comisiones_planes]
GO
ALTER TABLE [dbo].[cursos]  WITH CHECK ADD  CONSTRAINT [FK_cursos_comisiones] FOREIGN KEY([id_comision])
REFERENCES [dbo].[comisiones] ([id_comision])
GO
ALTER TABLE [dbo].[cursos] CHECK CONSTRAINT [FK_cursos_comisiones]
GO
ALTER TABLE [dbo].[cursos]  WITH CHECK ADD  CONSTRAINT [FK_cursos_materias] FOREIGN KEY([id_materia])
REFERENCES [dbo].[materias] ([id_materia])
GO
ALTER TABLE [dbo].[cursos] CHECK CONSTRAINT [FK_cursos_materias]
GO
ALTER TABLE [dbo].[docentes_cursos]  WITH CHECK ADD  CONSTRAINT [FK_docentes_cursos_cursos] FOREIGN KEY([id_curso])
REFERENCES [dbo].[cursos] ([id_curso])
GO
ALTER TABLE [dbo].[docentes_cursos] CHECK CONSTRAINT [FK_docentes_cursos_cursos]
GO
ALTER TABLE [dbo].[docentes_cursos]  WITH CHECK ADD  CONSTRAINT [FK_docentes_cursos_personas] FOREIGN KEY([id_docente])
REFERENCES [dbo].[personas] ([id_persona])
GO
ALTER TABLE [dbo].[docentes_cursos] CHECK CONSTRAINT [FK_docentes_cursos_personas]
GO
ALTER TABLE [dbo].[materias]  WITH CHECK ADD  CONSTRAINT [FK_materias_planes] FOREIGN KEY([id_plan])
REFERENCES [dbo].[planes] ([id_plan])
GO
ALTER TABLE [dbo].[materias] CHECK CONSTRAINT [FK_materias_planes]
GO
ALTER TABLE [dbo].[modulos_usuarios]  WITH CHECK ADD  CONSTRAINT [FK_modulos_usuarios_modulos] FOREIGN KEY([id_modulo])
REFERENCES [dbo].[modulos] ([id_modulo])
GO
ALTER TABLE [dbo].[modulos_usuarios] CHECK CONSTRAINT [FK_modulos_usuarios_modulos]
GO
ALTER TABLE [dbo].[modulos_usuarios]  WITH CHECK ADD  CONSTRAINT [FK_modulos_usuarios_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[modulos_usuarios] CHECK CONSTRAINT [FK_modulos_usuarios_usuarios]
GO
ALTER TABLE [dbo].[personas]  WITH CHECK ADD  CONSTRAINT [FK_personas_planes] FOREIGN KEY([id_plan])
REFERENCES [dbo].[planes] ([id_plan])
GO
ALTER TABLE [dbo].[personas] CHECK CONSTRAINT [FK_personas_planes]
GO
ALTER TABLE [dbo].[planes]  WITH CHECK ADD  CONSTRAINT [FK_planes_especialidades] FOREIGN KEY([id_especialidad])
REFERENCES [dbo].[especialidades] ([id_especialidad])
GO
ALTER TABLE [dbo].[planes] CHECK CONSTRAINT [FK_planes_especialidades]
GO
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD  CONSTRAINT [FK_usuarios_personas] FOREIGN KEY([id_persona])
REFERENCES [dbo].[personas] ([id_persona])
GO
ALTER TABLE [dbo].[usuarios] CHECK CONSTRAINT [FK_usuarios_personas]
GO
