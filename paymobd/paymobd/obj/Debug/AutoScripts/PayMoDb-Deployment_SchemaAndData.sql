SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pre_card](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[operator_name] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
	[pre_ammount] [float] NOT NULL,
	[pre_number] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wallet_deposit_temp](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
	[deposit_date] [datetime] NOT NULL,
	[deposit_ammount] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfiles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
	[e_mail] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
	[password] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
	[join_date] [datetime] NOT NULL,
	[wallet_money] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET IDENTITY_INSERT [dbo].[UserProfiles] ON 

GO
INSERT [dbo].[UserProfiles] ([ID], [username], [e_mail], [password], [join_date], [wallet_money]) VALUES (1, N'arif1613', N'arif1613@yahoo.com', N'aa1613', CAST(0x0000A20D005AB25C AS DateTime), CAST(2750.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[UserProfiles] ([ID], [username], [e_mail], [password], [join_date], [wallet_money]) VALUES (2, N'maruf', N'm198494@yahoo.com', N'ma1613', CAST(0x0000A20F002708BE AS DateTime), CAST(1000.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[UserProfiles] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_refill_history](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
	[send_to] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
	[sent_date] [datetime] NOT NULL,
	[ammount] [decimal](18, 2) NOT NULL,
	[status] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET IDENTITY_INSERT [dbo].[User_refill_history] ON 

GO
INSERT [dbo].[User_refill_history] ([ID], [username], [send_to], [sent_date], [ammount], [status]) VALUES (9, N'arif1613', N'123', CAST(0x0000A2120161B344 AS DateTime), CAST(100.00 AS Decimal(18, 2)), N'Pending')
GO
SET IDENTITY_INSERT [dbo].[User_refill_history] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_deposit_history](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
	[deposit_date] [datetime] NOT NULL,
	[deposit_ammount] [decimal](18, 2) NOT NULL,
	[deposit_status] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET IDENTITY_INSERT [dbo].[user_deposit_history] ON 

GO
INSERT [dbo].[user_deposit_history] ([ID], [username], [deposit_date], [deposit_ammount], [deposit_status]) VALUES (11, N'arif1613', CAST(0x0000A20E003FAA70 AS DateTime), CAST(1500.00 AS Decimal(18, 2)), N'Approved')
GO
INSERT [dbo].[user_deposit_history] ([ID], [username], [deposit_date], [deposit_ammount], [deposit_status]) VALUES (12, N'arif1613', CAST(0x0000A20E00418188 AS DateTime), CAST(2000.00 AS Decimal(18, 2)), N'Approved')
GO
INSERT [dbo].[user_deposit_history] ([ID], [username], [deposit_date], [deposit_ammount], [deposit_status]) VALUES (13, N'maruf', CAST(0x0000A20F00270894 AS DateTime), CAST(1500.00 AS Decimal(18, 2)), N'Approved')
GO
SET IDENTITY_INSERT [dbo].[user_deposit_history] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transections](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NOT NULL,
	[mo_number] [nvarchar](11) COLLATE Finnish_Swedish_CI_AS NOT NULL,
	[ConfirmNumber] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NOT NULL,
	[transection_ammount] [decimal](18, 2) NOT NULL,
	[transection_date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET IDENTITY_INSERT [dbo].[Transections] ON 

GO
INSERT [dbo].[Transections] ([Id], [username], [mo_number], [ConfirmNumber], [transection_ammount], [transection_date]) VALUES (1, N'arif1613', N'222', N'222', CAST(100.00 AS Decimal(18, 2)), CAST(0x0000A20D005ADA34 AS DateTime))
GO
INSERT [dbo].[Transections] ([Id], [username], [mo_number], [ConfirmNumber], [transection_ammount], [transection_date]) VALUES (2, N'arif1613', N'222', N'222', CAST(200.00 AS Decimal(18, 2)), CAST(0x0000A20D005ADA34 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Transections] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transection_final](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
	[send_to] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
	[sent_date] [datetime] NOT NULL,
	[ammount] [decimal](18, 2) NOT NULL,
	[status] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET IDENTITY_INSERT [dbo].[transection_final] ON 

GO
INSERT [dbo].[transection_final] ([ID], [username], [send_to], [sent_date], [ammount], [status]) VALUES (8, N'maruf', N'8152973', CAST(0x0000A20F002718FC AS DateTime), CAST(500.00 AS Decimal(18, 2)), N'Sent')
GO
INSERT [dbo].[transection_final] ([ID], [username], [send_to], [sent_date], [ammount], [status]) VALUES (12, N'arif1613', N'222', CAST(0x0000A21201619D00 AS DateTime), CAST(50.00 AS Decimal(18, 2)), N'Pending')
GO
SET IDENTITY_INSERT [dbo].[transection_final] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EdmMetadata](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModelHash] [nvarchar](max) COLLATE Finnish_Swedish_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET IDENTITY_INSERT [dbo].[EdmMetadata] ON 

GO
INSERT [dbo].[EdmMetadata] ([Id], [ModelHash]) VALUES (1, N'550DB66F4E5B45F8A010BCF2118E38765DBEE4F2AE2C9F980B2CD52B768D0379')
GO
SET IDENTITY_INSERT [dbo].[EdmMetadata] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[ApplicationName] [nvarchar](235) COLLATE Finnish_Swedish_CI_AS NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](256) COLLATE Finnish_Swedish_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
INSERT [dbo].[Applications] ([ApplicationName], [ApplicationId], [Description]) VALUES (N'/', N'87d4e98a-bf30-497e-8e76-3eee136d7e9a', NULL)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) COLLATE Finnish_Swedish_CI_AS NOT NULL,
	[IsAnonymous] [bit] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
INSERT [dbo].[Users] ([ApplicationId], [UserId], [UserName], [IsAnonymous], [LastActivityDate]) VALUES (N'87d4e98a-bf30-497e-8e76-3eee136d7e9a', N'cf50fc12-10df-4ff9-b27b-1cad69125a2d', N'maruf', 0, CAST(0x0000A210014E2A9A AS DateTime))
GO
INSERT [dbo].[Users] ([ApplicationId], [UserId], [UserName], [IsAnonymous], [LastActivityDate]) VALUES (N'87d4e98a-bf30-497e-8e76-3eee136d7e9a', N'e8a995a2-4a32-465e-b1ab-fc7fa9c67657', N'arif1613', 0, CAST(0x0000A212013EE6C2 AS DateTime))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](256) COLLATE Finnish_Swedish_CI_AS NOT NULL,
	[Description] [nvarchar](256) COLLATE Finnish_Swedish_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
INSERT [dbo].[Roles] ([ApplicationId], [RoleId], [RoleName], [Description]) VALUES (N'87d4e98a-bf30-497e-8e76-3eee136d7e9a', N'82ca5dff-08f2-4d0a-8ebd-c30c00ee4f59', N'aa1613', NULL)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersInRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
INSERT [dbo].[UsersInRoles] ([UserId], [RoleId]) VALUES (N'e8a995a2-4a32-465e-b1ab-fc7fa9c67657', N'82ca5dff-08f2-4d0a-8ebd-c30c00ee4f59')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Memberships](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Password] [nvarchar](128) COLLATE Finnish_Swedish_CI_AS NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [nvarchar](128) COLLATE Finnish_Swedish_CI_AS NOT NULL,
	[Email] [nvarchar](256) COLLATE Finnish_Swedish_CI_AS NULL,
	[PasswordQuestion] [nvarchar](256) COLLATE Finnish_Swedish_CI_AS NULL,
	[PasswordAnswer] [nvarchar](128) COLLATE Finnish_Swedish_CI_AS NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowsStart] [datetime] NOT NULL,
	[Comment] [nvarchar](256) COLLATE Finnish_Swedish_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
INSERT [dbo].[Memberships] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [Email], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowsStart], [Comment]) VALUES (N'87d4e98a-bf30-497e-8e76-3eee136d7e9a', N'cf50fc12-10df-4ff9-b27b-1cad69125a2d', N'SH9oWTwSyvOTB56irZ591PNOzrzLHai+6gq58lY/IbE=', 1, N'FGkRUpKZn+ctTh+R64ss4g==', N'm198494@yahoo.com', NULL, NULL, 1, 0, CAST(0x0000A20F0006127A AS DateTime), CAST(0x0000A210014E2A9A AS DateTime), CAST(0x0000A20F0006127A AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
GO
INSERT [dbo].[Memberships] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [Email], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowsStart], [Comment]) VALUES (N'87d4e98a-bf30-497e-8e76-3eee136d7e9a', N'e8a995a2-4a32-465e-b1ab-fc7fa9c67657', N'RYFfZC5Cq5djFib7Fra97Kex2R9sFF84rmjHU4ewZcM=', 1, N'tSiczmVrElHzBd52IZqjCg==', N'arif1613@yahoo.com', NULL, NULL, 1, 0, CAST(0x0000A20D0039BD08 AS DateTime), CAST(0x0000A212013EE6C2 AS DateTime), CAST(0x0000A20D0039BD08 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles](
	[UserId] [uniqueidentifier] NOT NULL,
	[PropertyNames] [nvarchar](4000) COLLATE Finnish_Swedish_CI_AS NOT NULL,
	[PropertyValueStrings] [nvarchar](4000) COLLATE Finnish_Swedish_CI_AS NOT NULL,
	[PropertyValueBinary] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [UserApplication] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [UserApplication]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [RoleApplication] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [RoleApplication]
GO
ALTER TABLE [dbo].[UsersInRoles]  WITH CHECK ADD  CONSTRAINT [UsersInRoleRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UsersInRoles] CHECK CONSTRAINT [UsersInRoleRole]
GO
ALTER TABLE [dbo].[UsersInRoles]  WITH CHECK ADD  CONSTRAINT [UsersInRoleUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UsersInRoles] CHECK CONSTRAINT [UsersInRoleUser]
GO
ALTER TABLE [dbo].[Memberships]  WITH CHECK ADD  CONSTRAINT [MembershipApplication] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[Memberships] CHECK CONSTRAINT [MembershipApplication]
GO
ALTER TABLE [dbo].[Memberships]  WITH CHECK ADD  CONSTRAINT [MembershipUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Memberships] CHECK CONSTRAINT [MembershipUser]
GO
ALTER TABLE [dbo].[Profiles]  WITH CHECK ADD  CONSTRAINT [UserProfile] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Profiles] CHECK CONSTRAINT [UserProfile]
GO
