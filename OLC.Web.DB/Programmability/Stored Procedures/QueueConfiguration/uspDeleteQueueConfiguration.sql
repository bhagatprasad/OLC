CREATE PROCEDURE [dbo].[uspDeleteQueueConfiguration]
	(
	  @queueConfigurationId     bigint
	)
	AS

	BEGIN

	SET NOCOUNT ON;

	update  [dbo].[QueueConfiguration]

	set 

	IsActive=0,
	 ModifiedOn = GETDATE() 
	 where Id=@queueConfigurationId;

	 END