CREATE DATABASE FAMILY_COUNCIL;
GO
USE FAMILY_COUNCIL;
GO


/****** Object:  Table [dbo].[tblCity]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCity](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[CountryID] [bigint] NULL,
 CONSTRAINT [PK_Sheet2$] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblFamilyStatus]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFamilyStatus](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](100) NULL,
 CONSTRAINT [PK_tblFamilyStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblForm]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblForm](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Url] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [bigint] NULL,
	[GoogleFormId] [nvarchar](200) NULL,
 CONSTRAINT [PK_tblForm] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblGender]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblGender](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](10) NULL,
 CONSTRAINT [PK_TblGender] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblLanguages]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLanguages](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ShortName] [nvarchar](10) NULL,
	[Name] [nvarchar](150) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_tblLanguages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPayment]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPayment](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[RequestedDate] [datetime] NULL,
	[RequestedPoints] [int] NULL,
	[PaymentMethod] [nvarchar](50) NULL,
	[ApprovedDate] [datetime] NULL,
	[Status] [varchar](20) NULL,
 CONSTRAINT [PK_tblPayment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPool]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPool](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PoolName] [nvarchar](50) NULL,
	[Participant] [int] NULL,
	[AgeMin] [int] NULL,
	[AgeMax] [int] NULL,
	[Gender] [char](10) NULL,
	[CityIds] [varchar](500) NULL,
	[FamilyStatus] [char](20) NULL,
 CONSTRAINT [PK_tblPoolTemplate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPoolSurvey]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPoolSurvey](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SurveyId] [bigint] NULL,
	[PoolTemplateId] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_tblPoolSurvey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSurvey]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSurvey](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FormId] [bigint] NULL,
	[DueDate] [datetime] NULL,
	[Title] [nvarchar](50) NULL,
	[Points] [int] NULL,
	[Participants] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[SurveyAnswer] [int] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_tblSurveyForm] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSurveyPoolCity]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSurveyPoolCity](
	[ID] [bigint] NOT NULL,
	[SurveyPoolID] [bigint] NULL,
	[CityID] [int] NULL,
 CONSTRAINT [PK_tblSurveyPoolCity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[JobTittle] [varchar](50) NULL,
	[MaritalStatus] [int] NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](max) NULL,
	[FullName] [nvarchar](50) NULL,
	[Gender] [char](1) NULL,
	[Age] [int] NULL,
	[Language] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[City] [bigint] NULL,
	[Status] [char](10) NULL,
	[IsEmailVerified] [bit] NULL,
	[CurrentCreditPoints] [int] NULL,
	[Role] [varchar](20) NULL,
	[Message] [nvarchar](max) NULL,
	[ProfilePicture] [varchar](100) NULL,
 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUserSurvey]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserSurvey](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SurveyID] [bigint] NULL,
	[PoolSurveyId] [bigint] NULL,
	[UserId] [bigint] NULL,
	[IsCompleted] [bit] NULL,
	[CompletedDate] [datetime] NULL,
	[SentDate] [datetime] NULL,
	[AwardedPoints] [int] NULL,
 CONSTRAINT [PK_TblUserSurvey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblPayment]  WITH CHECK ADD  CONSTRAINT [FK_tblPayment_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([ID])
GO
ALTER TABLE [dbo].[tblPayment] CHECK CONSTRAINT [FK_tblPayment_tblUser]
GO
ALTER TABLE [dbo].[tblPoolSurvey]  WITH CHECK ADD  CONSTRAINT [FK_tblPoolSurvey_tblPool] FOREIGN KEY([PoolTemplateId])
REFERENCES [dbo].[tblPool] ([ID])
GO
ALTER TABLE [dbo].[tblPoolSurvey] CHECK CONSTRAINT [FK_tblPoolSurvey_tblPool]
GO
ALTER TABLE [dbo].[tblPoolSurvey]  WITH CHECK ADD  CONSTRAINT [FK_tblPoolSurvey_tblSurvey] FOREIGN KEY([SurveyId])
REFERENCES [dbo].[tblSurvey] ([ID])
GO
ALTER TABLE [dbo].[tblPoolSurvey] CHECK CONSTRAINT [FK_tblPoolSurvey_tblSurvey]
GO
ALTER TABLE [dbo].[tblSurvey]  WITH CHECK ADD  CONSTRAINT [FK_tblSurvey_tblForm] FOREIGN KEY([FormId])
REFERENCES [dbo].[tblForm] ([ID])
GO
ALTER TABLE [dbo].[tblSurvey] CHECK CONSTRAINT [FK_tblSurvey_tblForm]
GO
ALTER TABLE [dbo].[tblUser]  WITH CHECK ADD  CONSTRAINT [FK_tblUser_tblCity] FOREIGN KEY([City])
REFERENCES [dbo].[tblCity] ([ID])
GO
ALTER TABLE [dbo].[tblUser] CHECK CONSTRAINT [FK_tblUser_tblCity]
GO
ALTER TABLE [dbo].[tblUserSurvey]  WITH CHECK ADD  CONSTRAINT [FK_tblUserSurvey_tblPoolSurvey] FOREIGN KEY([PoolSurveyId])
REFERENCES [dbo].[tblPoolSurvey] ([ID])
GO
ALTER TABLE [dbo].[tblUserSurvey] CHECK CONSTRAINT [FK_tblUserSurvey_tblPoolSurvey]
GO
ALTER TABLE [dbo].[tblUserSurvey]  WITH CHECK ADD  CONSTRAINT [FK_tblUserSurvey_tblSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[tblSurvey] ([ID])
GO
ALTER TABLE [dbo].[tblUserSurvey] CHECK CONSTRAINT [FK_tblUserSurvey_tblSurvey]
GO
/****** Object:  StoredProcedure [dbo].[GetActiveSurvey]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[GetActiveSurvey] 

AS    
select ID as Id
,FormId
,DueDate
,Title
,Points
,Participants
,CreatedDate
,SurveyAnswer
,IsDeleted from tblSurvey where DueDate > =GETDATE()
GO
/****** Object:  StoredProcedure [dbo].[GetArchiveSurvey]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[GetArchiveSurvey] 

AS    
declare @date datetime
set @date =GETDATE()
select ID as Id
,FormId
,DueDate
,Title
,Points
,Participants
,CreatedDate
,SurveyAnswer
,IsDeleted from tblSurvey where CAST(DueDate AS DATE)  < CAST(@date AS DATE) 
GO
/****** Object:  StoredProcedure [dbo].[SearchUserSurvey]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

    
CREATE PROCEDURE [dbo].[SearchUserSurvey]    
 @limit int    
,@AgeMin nvarchar(20)       
,@AgeMax nvarchar(20)       
,@City nvarchar(20)      
,@Gender nvarchar(20)      
,@Family nvarchar(20)  
,@UserIds nvarchar(max)  
     
      
AS      
Declare @where nvarchar(MAX)      
DECLARE @int AS int;    
DECLARE @Command NVARCHAR(MAX)
Declare @genderName Nvarchar(50)
Declare @FamilyStatusName Nvarchar(50) 
 --SET @int = CAST(@limit AS INT);    
 --select @Gender
 set @where =''
if @AgeMin <> '' and  @AgeMin is not null    
begin       
--select 'run'
set @where ='and u.Age between '+@AgeMin +'and '+  @AgeMax     
end      
 --select @where    
if @City is not null       
begin 
set @where =@where+'and u.City in ('+@City+')'       
end      
if @Gender is not null       
begin
   set @genderName =( select name from tblgender where id=@Gender)
 if(@genderName <>'any')
 begin
set @where =@where+'and u.Gender in ('+@Gender+')'  
end
--select 'zia'
end  
-- select @where
if @Family is not null       
begin       
   set @FamilyStatusName =( select name from tblfamilystatus where id=@Family)
 if(@FamilyStatusName <>'any')
 begin
set @where =@where+'and u.MaritalStatus in ('+@Family+')'        
end
end    
if @UserIds is not null and @userIds <>''  
begin  
set  @where =@where+'and u.id not in ('+@UserIds+')'   
end   
set @Command= 'select top ('+ convert(varchar(10),@limit) +') u.ID as id,u.JobTittle,u.MaritalStatus,u.Username,u.Password,u.FullName,u.Gender,u.Age,u.Language,u.Country,u.City,u.Status,u.IsEmailVerified,u.CurrentCreditPoints,u.Role,u.Message,u.ProfilePicture from tblUser u'    
    

set @where=@where +'and u.role=''User'' and status=''Approved'''    
set @where=@where +'order by NewID()'
set @where= RIGHT(@Where, LEN(@Where) - 4)    
--select @Where      
If( @Where <> '' or @Command <> ''  )      
begin      
   Set @Command = @Command + ' Where ' + @Where     
 --  select @Command as command    
Execute SP_ExecuteSQL  @Command      
end  
GO
/****** Object:  StoredProcedure [dbo].[SearchUserSurvey_old]    Script Date: 27/01/2021 12:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SearchUserSurvey_old]    
 @limit int    
,@AgeMin nvarchar(20)       
,@AgeMax nvarchar(20)       
,@City nvarchar(20)      
,@Gender nvarchar(20)      
,@Family nvarchar(20)  
,@UserIds nvarchar(max)  
     
      
AS      
Declare @where nvarchar(MAX)      
DECLARE @int AS int;    
DECLARE @Command NVARCHAR(MAX)      
 --SET @int = CAST(@limit AS INT);    
 --select @Gender
 set @where =''
if @AgeMin <> '' and  @AgeMin is not null    
begin       
--select 'run'      
set @where ='and u.Age between '+@AgeMin +'and '+  @AgeMax     
end      
 --select @where    
if @City is not null       
begin       
set @where =@where+'and u.City in ('+@City+')'       
end      
if @Gender is not null       
begin       
set @where =@where+'and u.Gender in ('+@Gender+')'  
--select 'zia'
end  
-- select @where
if @Family is not null       
begin       
set @where =@where+'and u.MaritalStatus in ('+@Family+')'        
end    
if @UserIds is not null and @userIds <>''  
begin  
set  @where =@where+'and u.id not in ('+@UserIds+')'   
end   
set @Command= 'select top ('+ convert(varchar(10),@limit) +') u.ID as id,u.JobTittle,u.MaritalStatus,u.Username,u.Password,u.FullName,u.Gender,u.Age,u.Language,u.Country,u.City,u.Status,u.IsEmailVerified,u.CurrentCreditPoints,u.Role,u.Message,u.ProfilePicture from tblUser u'    
    
set @where= RIGHT(@Where, LEN(@Where) - 4)    
set @where=@where +'and u.role=''User'' and status=''Approved'''    
set @where=@where +'order by NewID()'    
--select @Where      
If( @Where <> '' or @Command <> ''  )      
begin      
   Set @Command = @Command + ' Where ' + @Where     
 --  select @Command as command    
Execute SP_ExecuteSQL  @Command      
end  
GO
USE [master]
GO
ALTER DATABASE [FAMILY_COUNCIL] SET  READ_WRITE 
GO