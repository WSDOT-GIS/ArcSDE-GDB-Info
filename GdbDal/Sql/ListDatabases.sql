SELECT [name]
  FROM [master].[sys].[databases]
  WHERE [owner_sid] <> 0x01
  ORDER BY [name]