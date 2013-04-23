SELECT        i.Name, i.PhysicalName, i.Path, t.Name AS Type, t.UUID AS TypeUuid, t.ParentTypeID
FROM            dbo.GDB_ITEMS AS i INNER JOIN
                         dbo.GDB_ITEMTYPES AS t ON i.Type = t.UUID
WHERE        (t.Name LIKE 'Feature%')
ORDER BY i.Path