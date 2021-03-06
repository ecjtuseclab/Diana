USE [Diana]
GO
/****** Object:  Table [dbo].[workflownodeoperator]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workflownodeoperator](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[nodeactionid] [int] NOT NULL,
	[operatorid] [int] NOT NULL,
	[operatortype] [int] NOT NULL,
	[begintime] [datetime] NULL,
	[endtime] [datetime] NULL,
	[operatorstatus] [int] NULL,
	[operatorlock] [int] NULL,
	[nodeoperator] [nvarchar](256) NULL,
 CONSTRAINT [PK_workflownodeoperator] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workflownodeaction]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workflownodeaction](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[actionid] [int] NULL,
	[wfid] [int] NULL,
	[nodeactionname] [nvarchar](50) NULL,
	[nodeactionmemo] [nvarchar](256) NULL,
	[currentnodeid] [int] NOT NULL,
	[nextnodeid] [int] NOT NULL,
	[nastatus] [int] NULL,
	[begintime] [datetime] NULL,
	[endtime] [datetime] NULL,
	[nacondition] [nvarchar](1024) NULL,
	[nalock] [int] NULL,
	[nodeactioncode] [int] NULL,
	[nodetype] [int] NULL,
 CONSTRAINT [PK_wfstep_auth] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workflownode]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workflownode](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[wfid] [int] NOT NULL,
	[wfnodename] [nvarchar](128) NOT NULL,
	[wfnodememo] [nvarchar](128) NOT NULL,
	[wfnodeflag] [int] NULL,
	[wfnodebegintime] [datetime] NULL,
	[wfnodeendtime] [datetime] NULL,
	[wfnodestatus] [int] NULL,
	[wfnodelock] [int] NULL,
 CONSTRAINT [PK_wfstepdefine] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workflowinstancetracings]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workflowinstancetracings](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[instanceid] [int] NOT NULL,
	[startnode] [nvarchar](256) NOT NULL,
	[endnode] [nvarchar](256) NOT NULL,
	[executeaction] [nvarchar](256) NULL,
	[executer] [nvarchar](256) NULL,
	[executetime] [datetime] NULL,
	[remark] [nvarchar](1024) NULL,
 CONSTRAINT [PK_workflowinstancetracings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[workflowinstancetracings] ON
INSERT [dbo].[workflowinstancetracings] ([id], [instanceid], [startnode], [endnode], [executeaction], [executer], [executetime], [remark]) VALUES (5, 9, N'初始状态', N'待提交', N'新增', N'czs', CAST(0x0000A751009C3606 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[workflowinstancetracings] OFF
/****** Object:  Table [dbo].[workflowinstances]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workflowinstances](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[wfid] [int] NOT NULL,
	[ownertabledataid] [int] NOT NULL,
	[currentnodeid] [int] NOT NULL,
	[status] [int] NULL,
	[datalock] [int] NULL,
	[nodcode] [int] NULL,
 CONSTRAINT [PK_workflowinstances] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[workflowinstances] ON
INSERT [dbo].[workflowinstances] ([id], [wfid], [ownertabledataid], [currentnodeid], [status], [datalock], [nodcode]) VALUES (9, 2, 1, 2, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[workflowinstances] OFF
/****** Object:  Table [dbo].[workflow]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workflow](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[wfname] [nvarchar](50) NOT NULL,
	[wfmemo] [nvarchar](1024) NULL,
	[wfbegintime] [datetime] NULL,
	[wfstoptime] [datetime] NULL,
	[wfflag] [int] NOT NULL,
	[wfownertable] [nvarchar](128) NULL,
	[wfinstancestable] [nvarchar](128) NULL,
	[wfstatus] [int] NULL,
	[wflock] [int] NULL,
	[wffieldname] [nvarchar](256) NULL,
	[wfjsonstr] [nvarchar](max) NULL,
 CONSTRAINT [PK_wfdefine] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[workflow] ON
INSERT [dbo].[workflow] ([id], [wfname], [wfmemo], [wfbegintime], [wfstoptime], [wfflag], [wfownertable], [wfinstancestable], [wfstatus], [wflock], [wffieldname], [wfjsonstr]) VALUES (10, N'wfMaterial', N'物料工作流', CAST(0x0000A13900000000 AS DateTime), CAST(0x0000A6EE00000000 AS DateTime), 0, N'MaterialInventories', N'', 0, 0, N'', N'')
INSERT [dbo].[workflow] ([id], [wfname], [wfmemo], [wfbegintime], [wfstoptime], [wfflag], [wfownertable], [wfinstancestable], [wfstatus], [wflock], [wffieldname], [wfjsonstr]) VALUES (11, N'tantest', N'asda', CAST(0x0000A7C800000000 AS DateTime), CAST(0x0000A7C800000000 AS DateTime), 0, N'tantest', N'workflowinstancetracings', 0, 0, N'', NULL)
SET IDENTITY_INSERT [dbo].[workflow] OFF
/****** Object:  Table [dbo].[user_role]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_role](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[userid] [int] NOT NULL,
	[roleid] [int] NOT NULL,
	[rolescode] [int] NOT NULL,
 CONSTRAINT [PK_user_role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[user_role] ON
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (3, 22, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (5, 87, 20, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (6, 88, 27, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (7, 89, 27, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (8, 90, 27, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (9, 90, 31, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (10, 91, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (11, 91, 29, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (12, 92, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (13, 92, 29, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (14, 95, 27, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (15, 95, 29, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (16, 95, 31, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (17, 96, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (19, 23, 27, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (20, 23, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (22, 27, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (25, 23, 29, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (26, 23, 31, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (27, 32, 20, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (28, 32, 27, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (29, 32, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (30, 32, 29, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (31, 33, 27, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (32, 33, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (33, 97, 20, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (34, 97, 27, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (35, 97, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (37, 100, 32, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (38, 102, 32, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (40, 104, 20, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (41, 104, 27, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (42, 104, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (43, 104, 29, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (44, 105, 27, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (52, 108, 27, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (54, 105, 32, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (55, 108, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (56, 105, 40, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (57, 109, 20, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (59, 107, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (60, 107, 29, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (63, 43, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (64, 110, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (65, 110, 29, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (74, 35, 20, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (78, 45, 20, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (79, 45, 27, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (80, 45, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (84, 44, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (85, 25, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (86, 25, 29, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (90, 114, 20, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (104, 21, 28, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (108, 106, 27, 0)
INSERT [dbo].[user_role] ([id], [userid], [roleid], [rolescode]) VALUES (109, 115, 40, 0)
SET IDENTITY_INSERT [dbo].[user_role] OFF
/****** Object:  Table [dbo].[user_group]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_group](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[userid] [int] NOT NULL,
	[groupid] [int] NOT NULL,
 CONSTRAINT [PK_user_group] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[user_group] ON
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (2, 22, 105)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (3, 27, 106)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (4, 24, 106)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (6, 32, 108)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (7, 33, 108)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (9, 87, 22)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (10, 88, 104)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (11, 88, 105)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (12, 88, 106)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (13, 89, 104)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (14, 89, 105)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (15, 89, 106)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (16, 90, 107)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (17, 90, 108)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (18, 91, 106)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (19, 91, 107)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (20, 91, 108)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (21, 92, 25)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (22, 92, 106)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (24, 95, 104)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (25, 95, 105)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (26, 95, 106)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (27, 96, 23)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (28, 97, 22)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (30, 23, 104)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (32, 33, 18)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (33, 33, 19)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (34, 97, 18)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (35, 97, 19)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (36, 97, 23)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (41, 104, 18)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (42, 104, 19)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (43, 104, 23)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (45, 105, 18)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (46, 105, 19)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (61, 108, 106)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (62, 108, 107)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (64, 107, 107)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (65, 110, 105)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (66, 110, 106)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (67, 111, 18)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (68, 111, 19)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (69, 111, 104)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (76, 45, 18)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (77, 45, 19)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (78, 45, 104)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (82, 114, 18)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (83, 114, 19)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (84, 114, 104)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (111, 21, 18)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (112, 21, 105)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (117, 106, 106)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (118, 106, 107)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (119, 115, 18)
INSERT [dbo].[user_group] ([id], [userid], [groupid]) VALUES (120, 115, 19)
SET IDENTITY_INSERT [dbo].[user_group] OFF
/****** Object:  Table [dbo].[user]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[username] [nvarchar](64) NOT NULL,
	[password] [nvarchar](128) NOT NULL,
	[rolelist] [nvarchar](50) NULL,
	[grouplist] [nvarchar](50) NULL,
	[pubkey] [nvarchar](512) NULL,
	[prikey] [nvarchar](512) NULL,
	[photo] [nchar](512) NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[user] ON
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (21, N'kwy', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (23, N'kwy23', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N'管理员,黄金会员', N'生产部,', N'687C3B2ADCDBD0FE2AE27D6BB4A15384C49E2FB402B4FE6EE601C414B1B644D96B39F9F52CF083A04EAF26505FA1FEF19EE80A0B018945292F746937864BF799', N'0470E3E7C85C5F3998623CC106A92C7C61A3725518976E37A8E61EBF514A76B5', N'                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ')
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (43, N'xpp43', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N'管理员,超级管理员,黄金会员', N'', N'411A56B9A9F11740BB98F186DE22762D3532160F0A5A592FBC75AD38FD1407A161C2DD29D578879FD24D6FA89F2CAF7923A590B2FB5931C5A89F81EE1E7E151D', N'053DBB6D5B6F2932285EA0C28C9C1FF11837B0C64DA4F2F1DC063406E6E53C42', N'                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ')
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (87, N'admin', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N'', NULL, N'6908BFC5426F8E4EE65A77C33500BB8B7EA4F97FF92C2E5AC3D0007BE80607FA14379362A0D148C9DC5D65CDE803D69B43876BE8B1350FF8B09888824D8749E3', N'566AA53918EB30C7EF6616FC71288C6C3ADE56A21D4B9847E027DC93BFAF2C60', NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (105, N'zml', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', N'管理员,青铜会员', N'后勤部,销售部,', N'', N'', N'                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ')
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (106, N'zml666', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [rolelist], [grouplist], [pubkey], [prikey], [photo]) VALUES (115, N'TT', N'IHz0EFMvkqR97iRc6bEf9x9Xjr12PrO76kTr0EPQGPs=', NULL, NULL, N'5D68F1E8E36BB5A04D30CC75791C6DC3DBCB62032349E43BC202B85DEA416ED83BEF5004737DD61F181AC0A43CD8E569CB493DCC9A8A649E06BD610765BFB569', N'1698E78E5A83DFF0F207CA223A98A3407809FD66F4BBFBB511F9DDC6BC343705', NULL)
SET IDENTITY_INSERT [dbo].[user] OFF
/****** Object:  Table [dbo].[threeloginInfo]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[threeloginInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accessToken] [nvarchar](max) NULL,
	[area] [int] NULL,
	[code] [nvarchar](max) NULL,
	[expiresIn] [nvarchar](max) NULL,
	[idCode] [nvarchar](50) NULL,
	[location] [nvarchar](max) NULL,
	[sex] [int] NULL,
	[type] [int] NULL,
	[typeName] [nvarchar](50) NULL,
	[uid] [nvarchar](50) NULL,
	[useImg] [nvarchar](max) NULL,
	[useName] [nvarchar](50) NULL,
	[userId] [int] NULL,
	[refreshToken] [nvarchar](max) NULL,
 CONSTRAINT [PK_threeloginInfo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[threeloginInfo] ON
INSERT [dbo].[threeloginInfo] ([id], [accessToken], [area], [code], [expiresIn], [idCode], [location], [sex], [type], [typeName], [uid], [useImg], [useName], [userId], [refreshToken]) VALUES (17, N'21.24523aa6a103dfc298e5f686e50e8cb0.2592000.1519549360.4087661885-10712884', 0, N'41f41bb47d1364d698f52034ebce5616', NULL, NULL, NULL, 0, 3, N'BAIDU', N'4087661885', N'http://tb.himg.baidu.com/sys/portraitn/item/8bf3e8afb7e58fabe68891e88c83e697a0e69591e4b8b6e2a0', N'请叫我范无救丶', 0, NULL)
INSERT [dbo].[threeloginInfo] ([id], [accessToken], [area], [code], [expiresIn], [idCode], [location], [sex], [type], [typeName], [uid], [useImg], [useName], [userId], [refreshToken]) VALUES (18, N'2.00jSiZDH0l53WL7d8ac74a35SsMPLD', 0, N'16eb4302536d11a63937a6aa630c21d3', NULL, NULL, NULL, 0, 3, N'BAIDU', N'6465770609', N'http://tvax1.sinaimg.cn/crop.26.5.83.83.50/0073zIiJly8fnofpt1p5vj3046046t8n.jpg', N'请叫我范小米_16358', 0, NULL)
SET IDENTITY_INSERT [dbo].[threeloginInfo] OFF
/****** Object:  Table [dbo].[Test]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[id] [nchar](10) NOT NULL,
	[State] [nvarchar](256) NULL,
	[name] [nvarchar](256) NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Test] ([id], [State], [name]) VALUES (N'1         ', N'待提交', N'数据测试1')
/****** Object:  Table [dbo].[role_resource]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role_resource](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[roleid] [int] NOT NULL,
	[resoureceid] [int] NOT NULL,
 CONSTRAINT [PK_role_resource] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[role_resource] ON
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (498, 40, 1)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (499, 40, 5)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (500, 40, 37)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (501, 40, 38)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (502, 40, 39)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (503, 40, 40)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (504, 40, 41)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (505, 45, 1)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (506, 45, 5)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (507, 45, 37)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (508, 45, 38)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (509, 45, 39)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (510, 45, 40)
INSERT [dbo].[role_resource] ([id], [roleid], [resoureceid]) VALUES (511, 45, 41)
SET IDENTITY_INSERT [dbo].[role_resource] OFF
/****** Object:  Table [dbo].[role_action]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role_action](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[roleid] [int] NOT NULL,
	[actionid] [int] NOT NULL,
	[controlername] [nvarchar](50) NULL,
	[actionscode] [int] NOT NULL,
 CONSTRAINT [PK_role_action] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[role_action] ON
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (86, 45, 73, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (87, 45, 75, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (88, 45, 76, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (89, 45, 78, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (90, 45, 79, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (91, 45, 80, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (92, 45, 81, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (93, 45, 83, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (94, 45, 84, NULL, 0)
INSERT [dbo].[role_action] ([id], [roleid], [actionid], [controlername], [actionscode]) VALUES (95, 45, 85, NULL, 0)
SET IDENTITY_INSERT [dbo].[role_action] OFF
/****** Object:  Table [dbo].[role]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[rolename] [nvarchar](50) NOT NULL,
	[rolecode] [int] NOT NULL,
	[roleexpiretime] [datetime] NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[role] ON
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (20, N'超级管理员', 1, CAST(0x0000A86200000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (27, N'管理员', 2, CAST(0x0000A7CA00000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (28, N'黄金会员', 4, CAST(0x0000A7CA00000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (29, N'白金会员', 8, CAST(0x0000A7CA00000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (31, N'白银会员', 16, CAST(0x0000A7E900000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (32, N'青铜会员', 32, CAST(0x0000A7C900000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (40, N'黑铁会员', 64, CAST(0x0000A7C900000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (43, N'钻石会员', 65, CAST(0x0000A86200000000 AS DateTime))
INSERT [dbo].[role] ([id], [rolename], [rolecode], [roleexpiretime]) VALUES (45, N'测试角色', 2, CAST(0x0000A87800000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[role] OFF
/****** Object:  Table [dbo].[resource_copy]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[resource_copy](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[resourcename] [nvarchar](50) NOT NULL,
	[resourcetype] [int] NOT NULL,
	[resourceurl] [nvarchar](1024) NULL,
	[resourcescript] [nvarchar](1024) NULL,
	[resourceowner] [nvarchar](1024) NULL,
	[resourceleval] [int] NULL,
	[resourceleftico] [nvarchar](50) NULL,
	[resourcerightico] [nvarchar](50) NULL,
	[resourceclass] [nvarchar](50) NULL,
	[resourcenumber] [int] NULL,
	[order] [int] NULL,
	[description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[resource_copy] ON
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (1, N'系统管理', 1, N'', N'1', N'0', 1, N'fa fa-gears', N'fa arrow', N'', 0, 0, N'')
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (2, N'系统安全', 1, N'f22', N'1', N'0', 1, N'fa fa-desktop', N'fa arrow', N'', 0, 0, N'')
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (3, N'工作流', 1, N'', N'1', N'0', 1, N'fa fa-bar-chart-o', N'fa arrow', N'', 0, 0, N'')
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (4, N'常用实例', 1, N'', N'1', N'0', 1, N'fa fa-tags', N'fa arrow', N'', 0, 0, N'')
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (5, N'用户管理', 3, N'index.ashx?ctrl=user', N'1', N'1', 3, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (7, N'角色管理', 3, N'index.ashx?ctrl=role', N'1', N'1', 3, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (8, N'动作管理', 3, N'index.ashx?ctrl=action', N'1', N'1', 3, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (9, N'资源管理', 3, N'index.ashx?ctrl=resource', N'1', N'1', 3, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (10, N'分组管理', 3, N'index.ashx?ctrl=group', N'1', N'1', 3, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (11, N'数据字典', 3, N'index.ashx?ctrl=propertymapping', N'1', N'1', 3, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (12, N'区域管理', 3, N'index.ashx?ctrl=area', N'1', N'1', 3, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (13, N'数据库备份', 3, N'index.ashx?ctrl=backup', N'1', N'2', 3, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (14, N'工作流管理', 2, N'index.ashx?ctrl=workflow', N'1', N'3', 2, N'', N'', N'', 0, 0, N'')
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (17, N'节点动作', 2, N'index.ashx?ctrl=workflownodeaction', N'1', N'3', 2, N'', N'', N'', 0, 0, N'')
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (18, N'节点管理', 2, N'index.ashx?ctrl=workflownode', N'1', N'3', 2, N'', N'', N'', 0, 0, N'')
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (19, N'操作者管理', 2, N'index.ashx?ctrl=workflownodeoperator', N'1', N'3', 2, N'', N'', N'', 0, 0, N'')
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (20, N'文件2-2-1', 2, N'1', N'1', N'4', 2, NULL, N'fa arrow', NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (21, N'文件2-2-3', 2, N'1', N'1', N'4', 2, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (22, N'文件3-1-1', 2, N'1', N'1', N'20', 2, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (23, N'文件3-1-2', 2, N'1', N'1', N'20', 2, NULL, N'fa arrow', NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (24, N'文件3-2-1', 2, N'1', N'1', N'23', 2, NULL, N'fa arrow', NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (25, N'文件3-2-2', 2, N'1', N'1', N'23', 2, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (26, N'文件4-1-1', 2, N'1', N'1', N'24', 2, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (27, N'文件4-1-2', 2, N'1', N'1', N'24', 2, N'', N'', N'', 0, 0, N'')
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (28, N'文件4-2-1', 2, N'1', N'1', N'4', 2, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (29, N'文件4-2-2', 2, N'1', N'1', N'4', 2, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (133, N'系统管理', 1, N'', N'1', N'0', 1, N'fa fa-gears', N'fa arrow', N'', 0, 0, N'')
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (134, N'系统安全', 1, N'f22', N'1', N'0', 1, N'fa fa-desktop', N'fa arrow', N'', 0, 0, N'')
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (135, N'工作流', 1, N'', N'1', N'0', 1, N'fa fa-bar-chart-o', N'fa arrow', N'', 0, 0, N'')
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (136, N'常用实例', 1, N'', N'1', N'0', 1, N'fa fa-tags', N'fa arrow', N'', 0, 0, N'')
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (149, N'文件2-2-1', 2, N'1', N'1', N'4', 2, NULL, N'fa arrow', NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (150, N'文件2-2-3', 2, N'1', N'1', N'4', 2, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (151, N'文件3-1-1', 2, N'1', N'1', N'20', 2, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (152, N'文件3-1-2', 2, N'1', N'1', N'20', 2, NULL, N'fa arrow', NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (153, N'文件3-2-1', 2, N'1', N'1', N'23', 2, NULL, N'fa arrow', NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (154, N'文件3-2-2', 2, N'1', N'1', N'23', 2, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (155, N'文件4-1-1', 2, N'1', N'1', N'24', 2, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (156, N'文件4-1-2', 2, N'1', N'1', N'24', 2, N'', N'', N'', 0, 0, N'')
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (157, N'文件4-2-1', 2, N'1', N'1', N'4', 2, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[resource_copy] ([id], [resourcename], [resourcetype], [resourceurl], [resourcescript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description]) VALUES (158, N'文件4-2-2', 2, N'1', N'1', N'4', 2, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[resource_copy] OFF
/****** Object:  Table [dbo].[resource]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[resource](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[resourcename] [nvarchar](50) NOT NULL,
	[resourcetype] [int] NOT NULL,
	[resourceurl] [nvarchar](1024) NULL,
	[resourceBootstrapscript] [nvarchar](1024) NULL,
	[resourceowner] [nvarchar](1024) NULL,
	[resourceleval] [int] NULL,
	[resourceleftico] [nvarchar](50) NULL,
	[resourcerightico] [nvarchar](50) NULL,
	[resourceclass] [nvarchar](50) NULL,
	[resourcenumber] [int] NULL,
	[order] [int] NULL,
	[description] [nvarchar](max) NULL,
	[controlername] [nvarchar](50) NULL,
	[resourceAcescript] [nvarchar](1024) NULL,
 CONSTRAINT [PK_resource] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[resource] ON
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (1, N'系统管理', 1, NULL, N'1', N'0', 1, N'fa fa-gears', N'fa arrow', NULL, 0, 0, NULL, NULL, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (3, N'工作流', 1, NULL, N'1', N'0', 1, N'fa fa-bar-chart-o', N'fa arrow', N'', 0, 0, N'', NULL, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (5, N'用户管理', 1, N'index.ashx?ctrl=user', N'1', N'1', 2, NULL, NULL, NULL, NULL, NULL, NULL, N'user', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (7, N'角色管理', 1, N'index.ashx?ctrl=role', N'1', N'1', 2, NULL, NULL, NULL, NULL, NULL, NULL, N'role', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (8, N'动作管理', 1, N'index.ashx?ctrl=action', N'1', N'1', 2, NULL, NULL, NULL, NULL, NULL, NULL, N'action', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (9, N'资源管理', 1, N'index.ashx?ctrl=resource', N'1', N'1', 2, NULL, NULL, NULL, NULL, NULL, NULL, N'resource', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (10, N'分组管理', 1, N'index.ashx?ctrl=group', N'1', N'1', 2, NULL, NULL, NULL, NULL, NULL, NULL, N'group', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (11, N'数据字典', 1, N'index.ashx?ctrl=propertymapping', N'1', N'1', 2, NULL, NULL, NULL, NULL, NULL, NULL, N'propertymapping', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (12, N'区域管理', 1, N'index.ashx?ctrl=area', N'1', N'1', 2, NULL, NULL, NULL, NULL, NULL, NULL, N'area', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (13, N'数据库备份', 1, N'index.ashx?ctrl=backup', N'1', N'2', 2, NULL, NULL, NULL, NULL, NULL, NULL, N'backup', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (14, N'工作流管理', 1, N'index.ashx?ctrl=workflow', N'1', N'3', 2, N'', N'', N'', 0, 0, N'', N'workflow', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (17, N'节点动作', 1, N'index.ashx?ctrl=workflownodeaction', N'1', N'3', 2, N'', N'', N'', 0, 0, N'', N'workflownodeaction', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (19, N'操作者管理', 1, N'index.ashx?ctrl=workflownodeoperator', N'1', N'3', 2, N'', N'', N'', 0, 0, N'', N'workflownodeoperator', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (27, N'工作流节点', 1, N'index.ashx?ctrl=workflownode', N'1', N'3', 2, NULL, NULL, NULL, 0, 0, NULL, N'workflownode', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (35, N'文章管理', 1, NULL, N'1', N'0', 1, N'fa fa-gears', N'fa arrow', N'', 0, 0, N'', NULL, NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (36, N'文章', 1, N'index.ashx?ctrl=article', N'1', N'35', 2, NULL, NULL, NULL, NULL, NULL, NULL, N'article', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (37, N'新增用户', 3, NULL, N'btn_add()', N'5', 3, N'fa fa-plus', N'fa fa-plus', N'btn btn-primary dropdown-text', NULL, 1, NULL, N'user', N'click:Add')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (38, N'修改用户', 3, NULL, N'btn_edit()', N'5', 3, N'fa fa-pencil-square-o', N'', N'fa fa-pencil-square-o', NULL, 1, N'', N'user', N'click:Edit,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (39, N'删除用户', 3, NULL, N'btn_delete()', N'5', 3, N'fa fa-trash-o', NULL, N'fa fa-trash-o', NULL, 1, NULL, N'user', N'click:Delete,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (40, N'查看用户', 3, NULL, N'btn_details()', N'5', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-search-plus', NULL, 1, NULL, N'user', N'click:Detail,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (41, N'密码重置', 3, NULL, N'btn_revisepassword()', N'5', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-key', NULL, 1, NULL, N'user', N'click:OpenRevisePasswordDialog,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (42, N'新增角色', 3, NULL, N'btn_add()', N'7', 3, N'fa fa-plus', N'fa fa-plus', N'btn btn-primary dropdown-text', NULL, 1, NULL, N'role', N'click:Add')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (43, N'修改角色', 3, NULL, N'btn_edit()', N'7', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-pencil-square-o', NULL, 1, NULL, N'role', N'click:Edit,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (44, N'删除角色', 3, NULL, N'btn_delete()', N'7', 3, N'fa fa-trash-o', NULL, N'fa fa-trash-o', NULL, 1, NULL, N'role', N'click:Delete,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (45, N'查看角色', 3, NULL, N'btn_details()', N'7', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-search-plus', NULL, 1, NULL, N'role', N'click:Detail,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (46, N'新增资源', 3, NULL, N'btn_add()', N'9', 3, N'fa fa-plus', N'fa fa-plus', N'btn btn-primary dropdown-text', NULL, 1, NULL, N'resource', N'click:Add')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (47, N'修改资源', 3, NULL, N'btn_edit()', N'9', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-pencil-square-o', NULL, 1, NULL, N'resource', N'click:Edit,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (48, N'删除资源', 3, NULL, N'btn_delete()', N'9', 3, N'fa fa-trash-o', NULL, N'fa fa-trash-o', NULL, 1, NULL, N'resource', N'click:Delete,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (49, N'查看资源', 3, NULL, N'btn_details()', N'9', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-search-plus', NULL, 1, NULL, N'resource', N'click:Detail,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (50, N'新增分组', 3, NULL, N'btn_add()', N'10', 3, N'fa fa-plus', N'fa fa-plus', N'btn btn-primary dropdown-text', NULL, 1, NULL, N'group', N'click:Add')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (51, N'修改分组', 3, NULL, N'btn_edit()', N'10', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-pencil-square-o', NULL, 1, NULL, N'group', N'click:Edit,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (52, N'删除分组', 3, NULL, N'btn_delete()', N'10', 3, N'fa fa-trash-o', NULL, N'fa fa-trash-o', NULL, 1, NULL, N'group', N'click:Delete,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (53, N'查看分组', 3, NULL, N'btn_details()', N'10', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-search-plus', NULL, 1, NULL, N'group', N'click:Detail,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (54, N'新增字典', 3, NULL, N'btn_add()', N'11', 3, N'fa fa-plus', N'fa fa-plus', N'btn btn-primary dropdown-text', NULL, 1, NULL, N'propertymapping', N'click:Add')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (55, N'修改字典', 3, NULL, N'btn_edit()', N'11', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-pencil-square-o', NULL, 1, NULL, N'propertymapping', N'click:Edit,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (56, N'删除字典', 3, NULL, N'btn_delete()', N'11', 3, N'fa fa-trash-o', NULL, N'fa fa-trash-o', NULL, 1, NULL, N'propertymapping', N'click:Delete,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (57, N'查看字典', 3, NULL, N'btn_details()', N'11', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-search-plus', NULL, 1, NULL, N'propertymapping', N'click:Detail,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (58, N'新增区域', 3, NULL, N'btn_add()', N'12', 3, N'fa fa-plus', N'fa fa-plus', N'btn btn-primary dropdown-text', NULL, 1, NULL, N'area', N'click:Add')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (59, N'修改区域', 3, NULL, N'btn_edit()', N'12', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-pencil-square-o', NULL, 1, NULL, N'area', N'click:Edit,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (60, N'删除区域', 3, NULL, N'btn_delete()', N'12', 3, N'fa fa-trash-o', NULL, N'fa fa-trash-o', NULL, 1, NULL, N'area', N'click:Delete,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (61, N'查看区域', 3, NULL, N'btn_details()', N'12', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-search-plus', NULL, 1, NULL, N'area', N'click:Detail,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (62, N'新增工作流', 3, NULL, N'btn_add()', N'14', 3, N'fa fa-plus', N'fa fa-plus', N'btn btn-primary dropdown-text', NULL, 1, NULL, N'workflow', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (63, N'修改工作流', 3, NULL, N'btn_edit()', N'14', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-pencil-square-o', NULL, 1, NULL, N'workflow', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (64, N'删除工作流', 3, NULL, N'btn_delete()', N'14', 3, N'fa fa-trash-o', NULL, N'fa fa-trash-o', NULL, 1, NULL, N'workflow', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (65, N'查看工作流', 3, NULL, N'btn_details()', N'14', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-search-plus', NULL, 1, NULL, N'workflow', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (66, N'新增节点动作', 3, NULL, N'btn_add()', N'17', 3, N'fa fa-plus', N'fa fa-plus', N'btn btn-primary dropdown-text', NULL, 1, NULL, N'workflownodeaction', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (67, N'修改节点动作', 3, NULL, N'btn_edit()', N'17', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-pencil-square-o', NULL, 1, NULL, N'workflownodeaction', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (68, N'删除节点动作', 3, NULL, N'btn_delete()', N'17', 3, N'fa fa-trash-o', NULL, N'fa fa-trash-o', NULL, 1, NULL, N'workflownodeaction', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (69, N'查看节点动作', 3, NULL, N'btn_details()', N'17', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-search-plus', NULL, 1, NULL, N'workflownodeaction', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (70, N'新增节点', 3, NULL, N'btn_add()', N'27', 3, N'fa fa-plus', N'fa fa-plus', N'btn btn-primary dropdown-text', NULL, 1, NULL, N'workflownode', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (71, N'修改节点', 3, NULL, N'btn_edit()', N'27', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-pencil-square-o', NULL, 1, NULL, N'workflownode', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (72, N'删除节点', 3, NULL, N'btn_delete()', N'27', 3, N'fa fa-trash-o', NULL, N'fa fa-trash-o', NULL, 1, NULL, N'workflownode', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (73, N'查看节点', 3, NULL, N'btn_details()', N'27', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-search-plus', NULL, 1, NULL, N'workflownode', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (74, N'新增节点操作者', 3, NULL, N'btn_add()', N'19', 3, N'fa fa-plus', N'fa fa-plus', N'btn btn-primary dropdown-text', NULL, 1, NULL, N'workflownodeoperator', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (75, N'修改节点操作者', 3, NULL, N'btn_edit()', N'19', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-pencil-square-o', NULL, 1, NULL, N'workflownodeoperator', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (76, N'删除节点操作者', 3, NULL, N'btn_delete()', N'19', 3, N'fa fa-trash-o', NULL, N'fa fa-trash-o', NULL, 1, NULL, N'workflownodeoperator', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (77, N'查看节点操作者', 3, NULL, N'btn_details()', N'19', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-search-plus', NULL, 1, NULL, N'workflownodeoperator', NULL)
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (79, N'新增动作', 3, NULL, N'btn_add()', N'8', 3, N'fa fa-plus', NULL, N'btn btn-primary dropdown-text', NULL, 1, NULL, N'action', N'click:Add')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (80, N'修改动作', 3, NULL, N'btn_edit()', N'8', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-pencil-square-o', NULL, 1, NULL, N'action', N'click:Edit,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (81, N'删除动作', 3, NULL, N'btn_delete()', N'8', 3, N'fa fa-trash-o', NULL, N'fa fa-trash-o', NULL, 1, NULL, N'action', N'click:Delete,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (82, N'查看详情', 3, NULL, N'btn_details()', N'8', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-search-plus', NULL, 1, NULL, N'action', N'click:Detail,attr:{disabled:!DataTable.SelectedModel()}')
INSERT [dbo].[resource] ([id], [resourcename], [resourcetype], [resourceurl], [resourceBootstrapscript], [resourceowner], [resourceleval], [resourceleftico], [resourcerightico], [resourceclass], [resourcenumber], [order], [description], [controlername], [resourceAcescript]) VALUES (84, N'工作流流程设计', 3, NULL, N'btn_processdesign()', N'14', 3, N'fa fa-pencil-square-o', NULL, N'fa fa-pencil-square-o', NULL, 1, NULL, N'workflow', NULL)
SET IDENTITY_INSERT [dbo].[resource] OFF
/****** Object:  Table [dbo].[propertymapping]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[propertymapping](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[propertyName] [nvarchar](50) NULL,
	[propertyValue] [nvarchar](50) NULL,
	[propertyMeaning] [nvarchar](50) NULL,
	[remark] [nvarchar](50) NULL,
	[parentId] [int] NULL,
 CONSTRAINT [PK_propertymapping] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[propertymapping] ON
INSERT [dbo].[propertymapping] ([id], [propertyName], [propertyValue], [propertyMeaning], [remark], [parentId]) VALUES (1, N'gender', N'1', N'男', NULL, 4)
INSERT [dbo].[propertymapping] ([id], [propertyName], [propertyValue], [propertyMeaning], [remark], [parentId]) VALUES (2, N'gender', N'2', N'女', NULL, 4)
INSERT [dbo].[propertymapping] ([id], [propertyName], [propertyValue], [propertyMeaning], [remark], [parentId]) VALUES (9, N'type', NULL, N'帮助文档', N'article', 0)
INSERT [dbo].[propertymapping] ([id], [propertyName], [propertyValue], [propertyMeaning], [remark], [parentId]) VALUES (10, N'type', N'1', N'平台介绍', N'article', 9)
INSERT [dbo].[propertymapping] ([id], [propertyName], [propertyValue], [propertyMeaning], [remark], [parentId]) VALUES (11, N'type', N'2', N'使用说明', N'article', 9)
SET IDENTITY_INSERT [dbo].[propertymapping] OFF
/****** Object:  Table [dbo].[order]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[OrderNO] [nvarchar](50) NULL,
	[CustomName] [nvarchar](50) NULL,
	[SalesVolume] [float] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[order] ON
INSERT [dbo].[order] ([id], [OrderNO], [CustomName], [SalesVolume]) VALUES (1, N'1001', N'老王', 100)
INSERT [dbo].[order] ([id], [OrderNO], [CustomName], [SalesVolume]) VALUES (2, N'1002', N'老张', 120)
INSERT [dbo].[order] ([id], [OrderNO], [CustomName], [SalesVolume]) VALUES (3, N'1003', N'李四', 190)
INSERT [dbo].[order] ([id], [OrderNO], [CustomName], [SalesVolume]) VALUES (4, N'1004', N'卡卡', 230)
INSERT [dbo].[order] ([id], [OrderNO], [CustomName], [SalesVolume]) VALUES (5, N'1005', N'内马尔', 300)
SET IDENTITY_INSERT [dbo].[order] OFF
/****** Object:  Table [dbo].[group]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[group](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[groupname] [nvarchar](50) NOT NULL,
	[groupcode] [int] NULL,
 CONSTRAINT [PK_group] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[group] ON
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (18, N'后勤部', 1)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (19, N'销售部', 2)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (104, N'生产部', 4)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (105, N'管理部', 8)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (106, N'人事部', 16)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (107, N'技术部', 32)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (108, N'生产部', 64)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (111, N'测试部', 128)
INSERT [dbo].[group] ([id], [groupname], [groupcode]) VALUES (112, N'市场部', 256)
SET IDENTITY_INSERT [dbo].[group] OFF
/****** Object:  Table [dbo].[backup]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[backup](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[databasename] [nvarchar](50) NOT NULL,
	[backupname] [nvarchar](50) NOT NULL,
	[backupsize] [nvarchar](50) NULL,
	[backuptime] [datetime] NULL,
	[backuppersonnel] [nvarchar](50) NULL,
	[instructions] [nvarchar](max) NULL,
	[absolutepath] [nvarchar](max) NULL,
	[relativepath] [nvarchar](max) NULL,
 CONSTRAINT [PK_backup] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[backup] ON
INSERT [dbo].[backup] ([id], [databasename], [backupname], [backupsize], [backuptime], [backuppersonnel], [instructions], [absolutepath], [relativepath]) VALUES (41, N'Diana', N'hjghj', N'ghj', CAST(0x0000A75501595D48 AS DateTime), N'kwy', N'sdfs', N'Z:/hjghj.bak', N'DownFile/backup/hjghj.bak')
INSERT [dbo].[backup] ([id], [databasename], [backupname], [backupsize], [backuptime], [backuppersonnel], [instructions], [absolutepath], [relativepath]) VALUES (42, N'Diana', N'fdsd', N'zxz', CAST(0x0000A755015AF194 AS DateTime), N'kwy', N'sdfsd', N'Z:/fdsd.bak', N'DownFile/backup/fdsd.bak')
SET IDENTITY_INSERT [dbo].[backup] OFF
/****** Object:  Table [dbo].[article]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[article](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[inserttime] [datetime] NOT NULL,
	[edittime] [datetime] NOT NULL,
	[type] [int] NULL,
 CONSTRAINT [PK_article] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[article] ON
INSERT [dbo].[article] ([id], [title], [content], [inserttime], [edittime], [type]) VALUES (1, N'框架介绍', N'<p>Diana轻量级开发框架的思想是基于MVC框架，但去出MVC框架的系统结构和实现的复杂性。通过使用单入口模式，利用反射机制实现了模型(model)－视图(view)－控制器(controller)的逻辑、数据、界面显示的分离，使得框架加载性能相对于传统MVC框架较为高效。前台设计了两种页面风格(基于MVVM框架Knockout的ACE前端，基于Bootstrap开发的扁平化前端)。后台ORM采用SQL Sugar使是系统与数据库的操作更加灵活稳定。Diana轻量级开发框架有强大的权限管理，并且集成工作流引擎组件，支持可视化操作，使业务流程灵活可控。框架本身是一个可二次开发的开发平台，开发者既能通过在本框架上完成大部分的基础开发工作，也可以根据框架预留接口进行再次开发，本框架结构清晰、性能高、速度快、扩展性强、界面简单。</strong></p>', CAST(0x0000A871013F9D7C AS DateTime), CAST(0x0000A871013F9D7C AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[article] OFF
/****** Object:  Table [dbo].[area]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[area](
	[id] [int] NOT NULL,
	[parentid] [int] NOT NULL,
	[layers] [int] NULL,
	[encode] [nvarchar](50) NULL,
	[fullname] [nvarchar](50) NULL,
 CONSTRAINT [PK_area] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (110000, 0, 1, N'110000', N'北京')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (110100, 110000, 2, N'110100', N'北京市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (110200, 110000, 2, N'110200', N'测试')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (120000, 0, 1, N'120000', N'天津')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (120100, 120000, 2, N'120100', N'天津市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (130000, 0, 1, N'130000', N'河北省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (130100, 130000, 2, N'130100', N'石家庄市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (130200, 130000, 2, N'130200', N'唐山市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (130300, 130000, 2, N'130300', N'秦皇岛市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (130400, 130000, 2, N'130400', N'邯郸市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (130500, 130000, 2, N'130500', N'邢台市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (130600, 130000, 2, N'130600', N'保定市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (130700, 130000, 2, N'130700', N'张家口市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (130800, 130000, 2, N'130800', N'承德市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (130900, 130000, 2, N'130900', N'沧州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (131000, 130000, 2, N'131000', N'廊坊市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (131100, 130000, 2, N'131100', N'衡水市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (140000, 0, 1, N'140000', N'山西省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (140100, 140000, 2, N'140100', N'太原市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (140200, 140000, 2, N'140200', N'大同市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (140300, 140000, 2, N'140300', N'阳泉市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (140400, 140000, 2, N'140400', N'长治市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (140500, 140000, 2, N'140500', N'晋城市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (140600, 140000, 2, N'140600', N'朔州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (140700, 140000, 2, N'140700', N'晋中市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (140800, 140000, 2, N'140800', N'运城市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (140900, 140000, 2, N'140900', N'忻州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (141000, 140000, 2, N'141000', N'临汾市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (141100, 140000, 2, N'141100', N'吕梁市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (150000, 0, 1, N'150000', N'内蒙古自治区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (150100, 150000, 2, N'150100', N'呼和浩特市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (150200, 150000, 2, N'150200', N'包头市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (150300, 150000, 2, N'150300', N'乌海市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (150400, 150000, 2, N'150400', N'赤峰市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (150500, 150000, 2, N'150500', N'通辽市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (150600, 150000, 2, N'150600', N'鄂尔多斯市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (150700, 150000, 2, N'150700', N'呼伦贝尔市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (150800, 150000, 2, N'150800', N'巴彦淖尔市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (150900, 150000, 2, N'150900', N'乌兰察布市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (152200, 150000, 2, N'152200', N'兴安盟')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (152500, 150000, 2, N'152500', N'锡林郭勒盟')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (152900, 150000, 2, N'152900', N'阿拉善盟')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (210000, 0, 1, N'210000', N'辽宁省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (210100, 210000, 2, N'210100', N'沈阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (210200, 210000, 2, N'210200', N'大连市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (210300, 210000, 2, N'210300', N'鞍山市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (210400, 210000, 2, N'210400', N'抚顺市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (210500, 210000, 2, N'210500', N'本溪市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (210600, 210000, 2, N'210600', N'丹东市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (210700, 210000, 2, N'210700', N'锦州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (210800, 210000, 2, N'210800', N'营口市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (210900, 210000, 2, N'210900', N'阜新市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (211000, 210000, 2, N'211000', N'辽阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (211100, 210000, 2, N'211100', N'盘锦市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (211200, 210000, 2, N'211200', N'铁岭市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (211300, 210000, 2, N'211300', N'朝阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (211400, 210000, 2, N'211400', N'葫芦岛市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (220000, 0, 1, N'220000', N'吉林省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (220100, 220000, 2, N'220100', N'长春市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (220200, 220000, 2, N'220200', N'吉林市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (220300, 220000, 2, N'220300', N'四平市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (220400, 220000, 2, N'220400', N'辽源市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (220500, 220000, 2, N'220500', N'通化市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (220600, 220000, 2, N'220600', N'白山市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (220700, 220000, 2, N'220700', N'松原市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (220800, 220000, 2, N'220800', N'白城市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (222400, 220000, 2, N'222400', N'延边朝鲜族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (230000, 0, 1, N'230000', N'黑龙江省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (230100, 230000, 2, N'230100', N'哈尔滨市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (230200, 230000, 2, N'230200', N'齐齐哈尔市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (230300, 230000, 2, N'230300', N'鸡西市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (230400, 230000, 2, N'230400', N'鹤岗市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (230500, 230000, 2, N'230500', N'双鸭山市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (230600, 230000, 2, N'230600', N'大庆市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (230700, 230000, 2, N'230700', N'伊春市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (230800, 230000, 2, N'230800', N'佳木斯市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (230900, 230000, 2, N'230900', N'七台河市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (231000, 230000, 2, N'231000', N'牡丹江市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (231100, 230000, 2, N'231100', N'黑河市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (231200, 230000, 2, N'231200', N'绥化市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (232700, 230000, 2, N'232700', N'大兴安岭地区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (310000, 0, 1, N'310000', N'上海')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (310100, 310000, 2, N'310100', N'上海市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (320000, 0, 1, N'320000', N'江苏省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (320100, 320000, 2, N'320100', N'南京市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (320200, 320000, 2, N'320200', N'无锡市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (320300, 320000, 2, N'320300', N'徐州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (320400, 320000, 2, N'320400', N'常州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (320500, 320000, 2, N'320500', N'苏州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (320600, 320000, 2, N'320600', N'南通市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (320700, 320000, 2, N'320700', N'连云港市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (320800, 320000, 2, N'320800', N'淮安市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (320900, 320000, 2, N'320900', N'盐城市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (321000, 320000, 2, N'321000', N'扬州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (321100, 320000, 2, N'321100', N'镇江市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (321200, 320000, 2, N'321200', N'泰州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (321300, 320000, 2, N'321300', N'宿迁市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (330000, 0, 1, N'330000', N'浙江省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (330100, 330000, 2, N'330100', N'杭州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (330200, 330000, 2, N'330200', N'宁波市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (330300, 330000, 2, N'330300', N'温州市')
GO
print 'Processed 100 total records'
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (330400, 330000, 2, N'330400', N'嘉兴市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (330500, 330000, 2, N'330500', N'湖州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (330600, 330000, 2, N'330600', N'绍兴市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (330700, 330000, 2, N'330700', N'金华市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (330800, 330000, 2, N'330800', N'衢州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (330900, 330000, 2, N'330900', N'舟山市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (331000, 330000, 2, N'331000', N'台州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (331100, 330000, 2, N'331100', N'丽水市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (340000, 0, 1, N'340000', N'安徽省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (340100, 340000, 2, N'340100', N'合肥市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (340200, 340000, 2, N'340200', N'芜湖市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (340300, 340000, 2, N'340300', N'蚌埠市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (340400, 340000, 2, N'340400', N'淮南市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (340500, 340000, 2, N'340500', N'马鞍山市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (340600, 340000, 2, N'340600', N'淮北市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (340700, 340000, 2, N'340700', N'铜陵市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (340800, 340000, 2, N'340800', N'安庆市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (341000, 340000, 2, N'341000', N'黄山市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (341100, 340000, 2, N'341100', N'滁州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (341200, 340000, 2, N'341200', N'阜阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (341300, 340000, 2, N'341300', N'宿州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (341500, 340000, 2, N'341500', N'六安市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (341600, 340000, 2, N'341600', N'亳州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (341700, 340000, 2, N'341700', N'池州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (341800, 340000, 2, N'341800', N'宣城市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (350000, 0, 1, N'350000', N'福建省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (350100, 350000, 2, N'350100', N'福州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (350200, 350000, 2, N'350200', N'厦门市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (350300, 350000, 2, N'350300', N'莆田市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (350400, 350000, 2, N'350400', N'三明市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (350500, 350000, 2, N'350500', N'泉州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (350600, 350000, 2, N'350600', N'漳州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (350700, 350000, 2, N'350700', N'南平市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (350800, 350000, 2, N'350800', N'龙岩市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (350900, 350000, 2, N'350900', N'宁德市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (360000, 0, 1, N'360000', N'江西省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (360100, 360000, 2, N'360100', N'南昌市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (360200, 360000, 2, N'360200', N'景德镇市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (360300, 360000, 2, N'360300', N'萍乡市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (360400, 360000, 2, N'360400', N'九江市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (360500, 360000, 2, N'360500', N'新余市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (360600, 360000, 2, N'360600', N'鹰潭市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (360700, 360000, 2, N'360700', N'赣州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (360800, 360000, 2, N'360800', N'吉安市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (360900, 360000, 2, N'360900', N'宜春市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (360901, 360900, 3, N'360901', N'袁州区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (361000, 360000, 2, N'361000', N'抚州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (361100, 360000, 2, N'361100', N'上饶市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (370000, 0, 1, N'370000', N'山东省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (370100, 370000, 2, N'370100', N'济南市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (370200, 370000, 2, N'370200', N'青岛市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (370300, 370000, 2, N'370300', N'淄博市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (370400, 370000, 2, N'370400', N'枣庄市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (370500, 370000, 2, N'370500', N'东营市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (370600, 370000, 2, N'370600', N'烟台市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (370700, 370000, 2, N'370700', N'潍坊市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (370800, 370000, 2, N'370800', N'济宁市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (370900, 370000, 2, N'370900', N'泰安市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (371000, 370000, 2, N'371000', N'威海市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (371100, 370000, 2, N'371100', N'日照市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (371200, 370000, 2, N'371200', N'莱芜市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (371300, 370000, 2, N'371300', N'临沂市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (371400, 370000, 2, N'371400', N'德州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (371500, 370000, 2, N'371500', N'聊城市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (371600, 370000, 2, N'371600', N'滨州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (371700, 370000, 2, N'371700', N'菏泽市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (410000, 0, 1, N'410000', N'河南省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (410100, 410000, 2, N'410100', N'郑州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (410200, 410000, 2, N'410200', N'开封市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (410300, 410000, 2, N'410300', N'洛阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (410400, 410000, 2, N'410400', N'平顶山市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (410500, 410000, 2, N'410500', N'安阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (410600, 410000, 2, N'410600', N'鹤壁市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (410700, 410000, 2, N'410700', N'新乡市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (410800, 410000, 2, N'410800', N'焦作市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (410881, 410000, 2, N'410881', N'济源市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (410900, 410000, 2, N'410900', N'濮阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (411000, 410000, 2, N'411000', N'许昌市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (411100, 410000, 2, N'411100', N'漯河市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (411200, 410000, 2, N'411200', N'三门峡市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (411300, 410000, 2, N'411300', N'南阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (411400, 410000, 2, N'411400', N'商丘市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (411500, 410000, 2, N'411500', N'信阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (411600, 410000, 2, N'411600', N'周口市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (411700, 410000, 2, N'411700', N'驻马店市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (420000, 0, 1, N'420000', N'湖北省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (420100, 420000, 2, N'420100', N'武汉市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (420200, 420000, 2, N'420200', N'黄石市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (420300, 420000, 2, N'420300', N'十堰市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (420500, 420000, 2, N'420500', N'宜昌市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (420600, 420000, 2, N'420600', N'襄阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (420700, 420000, 2, N'420700', N'鄂州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (420800, 420000, 2, N'420800', N'荆门市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (420900, 420000, 2, N'420900', N'孝感市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (421000, 420000, 2, N'421000', N'荆州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (421100, 420000, 2, N'421100', N'黄冈市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (421200, 420000, 2, N'421200', N'咸宁市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (421300, 420000, 2, N'421300', N'随州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (422800, 420000, 2, N'422800', N'恩施土家族苗族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (430000, 0, 1, N'430000', N'湖南省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (430100, 430000, 2, N'430100', N'长沙市')
GO
print 'Processed 200 total records'
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (430200, 430000, 2, N'430200', N'株洲市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (430300, 430000, 2, N'430300', N'湘潭市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (430400, 430000, 2, N'430400', N'衡阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (430500, 430000, 2, N'430500', N'邵阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (430600, 430000, 2, N'430600', N'岳阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (430700, 430000, 2, N'430700', N'常德市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (430800, 430000, 2, N'430800', N'张家界市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (430900, 430000, 2, N'430900', N'益阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (431000, 430000, 2, N'431000', N'郴州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (431100, 430000, 2, N'431100', N'永州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (431200, 430000, 2, N'431200', N'怀化市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (431300, 430000, 2, N'431300', N'娄底市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (433100, 430000, 2, N'433100', N'湘西土家族苗族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (440000, 0, 1, N'440000', N'广东省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (440100, 440000, 2, N'440100', N'广州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (440200, 440000, 2, N'440200', N'韶关市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (440300, 440000, 2, N'440300', N'深圳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (440400, 440000, 2, N'440400', N'珠海市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (440500, 440000, 2, N'440500', N'汕头市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (440600, 440000, 2, N'440600', N'佛山市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (440700, 440000, 2, N'440700', N'江门市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (440800, 440000, 2, N'440800', N'湛江市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (440900, 440000, 2, N'440900', N'茂名市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (441200, 440000, 2, N'441200', N'肇庆市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (441300, 440000, 2, N'441300', N'惠州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (441400, 440000, 2, N'441400', N'梅州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (441500, 440000, 2, N'441500', N'汕尾市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (441600, 440000, 2, N'441600', N'河源市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (441700, 440000, 2, N'441700', N'阳江市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (441800, 440000, 2, N'441800', N'清远市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (445100, 440000, 2, N'445100', N'潮州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (445200, 440000, 2, N'445200', N'揭阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (445300, 440000, 2, N'445300', N'云浮市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (450000, 0, 1, N'450000', N'广西壮族自治区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (450100, 450000, 2, N'450100', N'南宁市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (450200, 450000, 2, N'450200', N'柳州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (450300, 450000, 2, N'450300', N'桂林市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (450400, 450000, 2, N'450400', N'梧州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (450500, 450000, 2, N'450500', N'北海市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (450600, 450000, 2, N'450600', N'防城港市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (450700, 450000, 2, N'450700', N'钦州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (450800, 450000, 2, N'450800', N'贵港市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (450900, 450000, 2, N'450900', N'玉林市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (451000, 450000, 2, N'451000', N'百色市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (451100, 450000, 2, N'451100', N'贺州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (451200, 450000, 2, N'451200', N'河池市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (451300, 450000, 2, N'451300', N'来宾市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (451400, 450000, 2, N'451400', N'崇左市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (460000, 0, 1, N'460000', N'海南省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (460100, 460000, 2, N'460100', N'海口市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (500000, 0, 1, N'500000', N'重庆')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (500100, 500000, 2, N'500100', N'重庆市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (510000, 0, 1, N'510000', N'四川省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (510100, 510000, 2, N'510100', N'成都市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (510300, 510000, 2, N'510300', N'自贡市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (510400, 510000, 2, N'510400', N'攀枝花市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (510500, 510000, 2, N'510500', N'泸州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (510600, 510000, 2, N'510600', N'德阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (510700, 510000, 2, N'510700', N'绵阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (510800, 510000, 2, N'510800', N'广元市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (510900, 510000, 2, N'510900', N'遂宁市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (511000, 510000, 2, N'511000', N'内江市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (511100, 510000, 2, N'511100', N'乐山市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (511300, 510000, 2, N'511300', N'南充市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (511400, 510000, 2, N'511400', N'眉山市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (511500, 510000, 2, N'511500', N'宜宾市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (511600, 510000, 2, N'511600', N'广安市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (511700, 510000, 2, N'511700', N'达州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (511800, 510000, 2, N'511800', N'雅安市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (511900, 510000, 2, N'511900', N'巴中市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (512000, 510000, 2, N'512000', N'资阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (513200, 510000, 2, N'513200', N'阿坝藏族羌族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (513300, 510000, 2, N'513300', N'甘孜藏族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (513400, 510000, 2, N'513400', N'凉山彝族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (520000, 0, 1, N'520000', N'贵州省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (520100, 520000, 2, N'520100', N'贵阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (520200, 520000, 2, N'520200', N'六盘水市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (520300, 520000, 2, N'520300', N'遵义市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (520400, 520000, 2, N'520400', N'安顺市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (522200, 520000, 2, N'522200', N'铜仁市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (522300, 520000, 2, N'522300', N'黔西南布依族苗族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (522400, 520000, 2, N'522400', N'毕节市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (522600, 520000, 2, N'522600', N'黔东南苗族侗族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (522700, 520000, 2, N'522700', N'黔南布依族苗族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (530000, 0, 1, N'530000', N'云南省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (530100, 530000, 2, N'530100', N'昆明市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (530300, 530000, 2, N'530300', N'曲靖市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (530400, 530000, 2, N'530400', N'玉溪市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (530500, 530000, 2, N'530500', N'保山市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (530600, 530000, 2, N'530600', N'昭通市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (530700, 530000, 2, N'530700', N'丽江市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (530800, 530000, 2, N'530800', N'普洱市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (530900, 530000, 2, N'530900', N'临沧市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (532300, 530000, 2, N'532300', N'楚雄彝族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (532500, 530000, 2, N'532500', N'红河哈尼族彝族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (532600, 530000, 2, N'532600', N'文山壮族苗族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (532800, 530000, 2, N'532800', N'西双版纳傣族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (532900, 530000, 2, N'532900', N'大理白族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (533100, 530000, 2, N'533100', N'德宏傣族景颇族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (533300, 530000, 2, N'533300', N'怒江傈僳族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (533400, 530000, 2, N'533400', N'迪庆藏族自治州')
GO
print 'Processed 300 total records'
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (540000, 0, 1, N'540000', N'西藏自治区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (540100, 540000, 2, N'540100', N'拉萨市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (542100, 540000, 2, N'542100', N'昌都地区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (542200, 540000, 2, N'542200', N'山南地区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (542300, 540000, 2, N'542300', N'日喀则地区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (542400, 540000, 2, N'542400', N'那曲地区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (542500, 540000, 2, N'542500', N'阿里地区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (542600, 540000, 2, N'542600', N'林芝地区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (610000, 0, 1, N'610000', N'陕西省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (610100, 610000, 2, N'610100', N'西安市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (610200, 610000, 2, N'610200', N'铜川市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (610300, 610000, 2, N'610300', N'宝鸡市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (610400, 610000, 2, N'610400', N'咸阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (610500, 610000, 2, N'610500', N'渭南市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (610600, 610000, 2, N'610600', N'延安市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (610700, 610000, 2, N'610700', N'汉中市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (610800, 610000, 2, N'610800', N'榆林市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (610900, 610000, 2, N'610900', N'安康市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (611000, 610000, 2, N'611000', N'商洛市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (620000, 0, 1, N'620000', N'甘肃省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (620100, 620000, 2, N'620100', N'兰州市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (620200, 620000, 2, N'620200', N'嘉峪关市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (620300, 620000, 2, N'620300', N'金昌市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (620400, 620000, 2, N'620400', N'白银市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (620500, 620000, 2, N'620500', N'天水市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (620600, 620000, 2, N'620600', N'武威市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (620700, 620000, 2, N'620700', N'张掖市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (620800, 620000, 2, N'620800', N'平凉市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (620900, 620000, 2, N'620900', N'酒泉市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (621000, 620000, 2, N'621000', N'庆阳市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (621100, 620000, 2, N'621100', N'定西市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (621200, 620000, 2, N'621200', N'陇南市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (622900, 620000, 2, N'622900', N'临夏回族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (623000, 620000, 2, N'623000', N'甘南藏族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (630000, 0, 1, N'630000', N'青海省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (630100, 630000, 2, N'630100', N'西宁市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (632100, 630000, 2, N'632100', N'海东市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (632200, 630000, 2, N'632200', N'海北藏族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (632300, 630000, 2, N'632300', N'黄南藏族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (632500, 630000, 2, N'632500', N'海南藏族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (632600, 630000, 2, N'632600', N'果洛藏族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (632700, 630000, 2, N'632700', N'玉树藏族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (632800, 630000, 2, N'632800', N'海西蒙古族藏族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (640000, 0, 1, N'640000', N'宁夏回族自治区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (640100, 640000, 2, N'640100', N'银川市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (640200, 640000, 2, N'640200', N'石嘴山市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (640300, 640000, 2, N'640300', N'吴忠市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (640400, 640000, 2, N'640400', N'固原市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (640500, 640000, 2, N'640500', N'中卫市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (650000, 0, 1, N'650000', N'新疆维吾尔自治区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (650100, 650000, 2, N'650100', N'乌鲁木齐市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (650200, 650000, 2, N'650200', N'克拉玛依市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (652100, 650000, 2, N'652100', N'吐鲁番地区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (652200, 650000, 2, N'652200', N'哈密地区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (652300, 650000, 2, N'652300', N'昌吉回族自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (652700, 650000, 2, N'652700', N'博尔塔拉蒙古自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (652800, 650000, 2, N'652800', N'巴音郭楞蒙古自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (652900, 650000, 2, N'652900', N'阿克苏地区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (653000, 650000, 2, N'653000', N'克孜勒苏柯尔克孜自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (653100, 650000, 2, N'653100', N'喀什地区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (653200, 650000, 2, N'653200', N'和田地区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (654000, 650000, 2, N'654000', N'伊犁哈萨克自治州')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (654200, 650000, 2, N'654200', N'塔城地区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (654300, 650000, 2, N'654300', N'阿勒泰地区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (810000, 0, 1, N'810000', N'香港特别行政区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (810100, 810000, 2, N'810100', N'香港岛')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (810200, 810000, 2, N'810200', N'九龙')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (810300, 810000, 2, N'810300', N'新界')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (820000, 0, 1, N'820000', N'澳门特别行政区')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (820100, 820000, 2, N'820100', N'澳门半岛')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (820300, 820000, 2, N'820300', N'路环岛')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (820400, 820000, 2, N'820400', N'凼仔岛')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (830000, 0, 1, N'830000', N'台湾省')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (830100, 830000, 2, N'830100', N'台北市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (830200, 830000, 2, N'830200', N'高雄市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (830300, 830000, 2, N'830300', N'台南市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (830400, 830000, 2, N'830400', N'台中市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (830500, 830000, 2, N'830500', N'南投县')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (830600, 830000, 2, N'830600', N'基隆市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (830700, 830000, 2, N'830700', N'新竹市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (830800, 830000, 2, N'830800', N'嘉义市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (830900, 830000, 2, N'830900', N'宜兰县')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (831000, 830000, 2, N'831000', N'新竹县')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (831100, 830000, 2, N'831100', N'桃园县')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (831200, 830000, 2, N'831200', N'苗栗县')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (831300, 830000, 2, N'831300', N'彰化县')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (831400, 830000, 2, N'831400', N'嘉义县')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (831500, 830000, 2, N'831500', N'云林县')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (831600, 830000, 2, N'831600', N'屏东县')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (831700, 830000, 2, N'831700', N'台东县')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (831800, 830000, 2, N'831800', N'花莲县')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (831900, 830000, 2, N'831900', N'澎湖县')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (832000, 830000, 2, N'832000', N'新北市')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (832100, 830000, 2, N'832100', N'台中县')
INSERT [dbo].[area] ([id], [parentid], [layers], [encode], [fullname]) VALUES (832200, 830000, 2, N'832200', N'连江县')
/****** Object:  Table [dbo].[action]    Script Date: 01/28/2018 20:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[action](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[actionname] [nvarchar](50) NOT NULL,
	[actionurl] [nvarchar](1024) NULL,
	[actionparam] [nvarchar](1024) NULL,
	[actiontype] [int] NULL,
	[actionowner] [nvarchar](50) NULL,
	[actioncode] [int] NOT NULL,
	[controlername] [nvarchar](50) NOT NULL,
	[actiondescription] [nvarchar](50) NULL,
 CONSTRAINT [PK_action] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[action] ON
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (73, N'用户管理', N'../index.ashx?ctrl=user', N'ctrl=user', 1, N'0', 1, N'userControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (75, N'新增', N'../index.ashx?ctrl=user&action=Form', N'ctrl=user&action=Form', 3, N'73', 1, N'userControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (76, N'修改', N'../index.ashx?ctrl=user&action=Formedit', N'ctrl=user&action=Formedit', 3, N'73', 2, N'userControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (78, N'删除', N'../index.ashx?ctrl=user&action=DeleteForm', N'ctrl=user&action=DeleteForm', 3, N'73', 4, N'userControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (79, N'详情', N'../index.ashx?ctrl=user&action=Details', N'ctrl=user&action=Details', 3, N'73', 8, N'userControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (80, N'角色管理', N'../index.ashx?ctrl=role', N'ctrl=role', 1, N'0', 1, N'roleControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (81, N'新增', N'../index.ashx?ctrl=role&action=Form', N'ctrl=role&action=Form', 3, N'80', 1, N'roleControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (83, N'修改', N'../index.ashx?ctrl=role&action=Form', N'ctrl=role&action=Form', 3, N'80', 2, N'roleControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (84, N'删除', N'../index.ashx?ctrl=role&action=DeleteForm', N'ctrl=role&action=DeleteForm', 3, N'80', 4, N'roleControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (85, N'详情', N'../index.ashx?ctrl=role&action=Details', N'ctrl=role&action=Details', 3, N'80', 8, N'roleControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (89, N'资源管理', N'../index.ashx?ctrl=resource', N'ctrl=resource', 1, N'0', 1, N'resourceControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (93, N'新增', N'../index.ashx?ctrl=resource&action=Form', N'ctrl=resource&action=Form', 3, N'89', 1, N'resourceControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (94, N'修改', N'../index.ashx?ctrl=resource&action=Form', N'ctrl=resource&action=Form', 3, N'89', 2, N'resourceControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (95, N'删除', N'../index.ashx?ctrl=resource&action=DeleteForm', N'ctrl=resource&action=DeleteForm', 3, N'89', 4, N'resourceControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (96, N'详情', N'../index.ashx?ctrl=resource&action=Details', N'ctrl=resource&action=Details', 3, N'89', 8, N'resourceControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (101, N'分组管理', N'../index.ashx?ctrl=group', N'ctrl=group', 1, N'0', 1, N'groupControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (102, N'新增', N'../index.ashx?ctrl=group&action=Form', N'ctrl=groupe&action=Form', 3, N'101', 1, N'groupControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (103, N'修改', N'../index.ashx?ctrl=group&action=Form', N'ctrl=group&action=Form', 3, N'101', 2, N'groupControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (104, N'删除', N'../index.ashx?ctrl=group&action=DeleteForm', N'ctrl=group&action=DeleteForm', 3, N'101', 4, N'groupControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (105, N'详情', N'../index.ashx?ctrl=group&action=Details', N'ctrl=group&action=Details', 3, N'101', 8, N'groupControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (106, N'数据字典管理', N'../index.ashx?ctrl=propertymapping', N'ctrl=propertymapping', 1, N'0', 1, N'groupControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (107, N'新增', N'../index.ashx?ctrl=propertymapping&action=Form', N'ctrl=propertymapping&action=Form', 3, N'106', 1, N'propertymappingControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (108, N'修改', N'../index.ashx?ctrl=propertymapping&action=Form', N'ctrl=propertymapping&action=Form', 3, N'106', 2, N'propertymappingControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (109, N'删除', N'../index.ashx?ctrl=propertymapping&action=DeleteForm', N'ctrl=propertymapping&action=DeleteForm', 3, N'106', 4, N'propertymappingControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (110, N'详情', N'../index.ashx?ctrl=propertymapping&action=Details', N'ctrl=propertymapping&action=Details', 3, N'106', 8, N'propertymappingControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (111, N'区域管理', N'../index.ashx?ctrl=area', N'ctrl=group', 1, N'0', 1, N'areaControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (112, N'新增', N'../index.ashx?ctrl=area&action=Form', N'ctrl=area&action=Form', 3, N'111', 1, N'areaControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (113, N'修改', N'../index.ashx?ctrl=area&action=Form', N'ctrl=area&action=Form', 3, N'111', 2, N'areaControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (114, N'删除', N'../index.ashx?ctrl=area&action=DeleteForm', N'ctrl=areap&action=DeleteForm', 3, N'111', 4, N'areaControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (115, N'详情', N'../index.ashx?ctrl=area&action=Details', N'ctrl=area&action=Details', 3, N'111', 8, N'areaControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (116, N'工作流管理', N'../index.ashx?ctrl=workflow', N'ctrl=workflow', 3, N'0', 3, N'workflowControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (117, N'新增', N'../index.ashx?ctrl=workflow&action=Form', N'ctrl=workflow&action=Form', 3, N'116', 5, N'workflowControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (118, N'修改', N'../index.ashx?ctrl=workflow&action=Form', N'ctrl=workflow&action=Form', 3, N'116', 4, N'workflowControler', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (121, N'新增', NULL, NULL, 2, N'wfMaterial', 2, N'workflow', N'新增')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (122, N'提交', NULL, NULL, 2, N'wfMaterial', 4, N'workflow', N'提交')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (123, N'审核', NULL, NULL, 2, N'wfMaterial', 8, N'workflow', N'审核')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (124, N'退回', NULL, NULL, 2, N'wfMaterial', 16, N'workflow', N'退回')
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (125, N'数据字典', N'../index.ashx?ctrl=PropertyMapping&action=PropertyMapping', N'PropertyMapping', 1, N'0', 1, N'PropertyMapping', NULL)
INSERT [dbo].[action] ([id], [actionname], [actionurl], [actionparam], [actiontype], [actionowner], [actioncode], [controlername], [actiondescription]) VALUES (126, N'新增', N'../index.ashx?ctrl=PropertyMapping&action=Add', N'action=Add', 1, N'125', 1, N'PropertyMapping', NULL)
SET IDENTITY_INSERT [dbo].[action] OFF
