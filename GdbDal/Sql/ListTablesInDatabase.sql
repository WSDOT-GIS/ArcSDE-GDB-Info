SELECT [name]
	  --,[object_id]
	  --,[principal_id]
	  --,[schema_id]
	  --,[parent_object_id]
	  --,[type]
	  --,[type_desc]
	  --,[create_date]
	  --,[modify_date]
	  --,[is_ms_shipped]
	  --,[is_published]
	  --,[is_schema_published]
  FROM [LRS].[sys].[all_objects]
  where 
	[type] LIKE '%U' AND [name] NOT LIKE 'SDE_GEOMETRY%'
	AND [name] NOT LIKE 'GDB_%' and [name] NOT LIKE 'SDE_%'
  ORDER BY name