CREATE DATABASE hair_salon
GO
USE [hair_salon]
GO
/****** Object:  Table [dbo].[clients]    Script Date: 7/15/2016 5:21:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[clients](
	[name] [varchar](255) NULL,
	[stylist_id] [int] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stylist]    Script Date: 7/15/2016 5:21:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stylist](
	[name] [varchar](255) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[stylist] ON 

INSERT [dbo].[stylist] ([name], [id]) VALUES (N'sam', 1)
INSERT [dbo].[stylist] ([name], [id]) VALUES (N'sam', 2)
INSERT [dbo].[stylist] ([name], [id]) VALUES (N'sam', 3)
INSERT [dbo].[stylist] ([name], [id]) VALUES (N'sam', 4)
INSERT [dbo].[stylist] ([name], [id]) VALUES (N'', 5)
INSERT [dbo].[stylist] ([name], [id]) VALUES (N'', 6)
INSERT [dbo].[stylist] ([name], [id]) VALUES (N'sami', 7)
INSERT [dbo].[stylist] ([name], [id]) VALUES (N'sami', 8)
INSERT [dbo].[stylist] ([name], [id]) VALUES (N'carl', 9)
INSERT [dbo].[stylist] ([name], [id]) VALUES (N'terry', 10)
SET IDENTITY_INSERT [dbo].[stylist] OFF
