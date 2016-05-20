
  
  --SELECT Distinct cdmcode,cdmdesc,ndc,score 
  --  FROM (
  --      SELECT cdmcode,cdmdesc,ndc,score , Rank() 
  --        over (Partition BY cdmcode
  --              ORDER BY score DESC ) AS Rank
  --      FROM combinedtable
  --      ) rs WHERE Rank <= 2
        
    --    SELECT rs.Field1,rs.Field2 
    --FROM (
    --    SELECT Field1,Field2, Rank() 
    --      over (Partition BY Section
    --            ORDER BY RankCriteria DESC ) AS Rank
    --    FROM table
    --    ) rs WHERE Rank <= 10
    
-- SELECT *
--FROM combinedtable1 s
--WHERE 
--        (
--            SELECT  COUNT(*) 
--            FROM    combinedtable1 f
--            WHERE f.cdmcode = s.cdmcode AND 
--                  f.score >= s.score
--        ) <= 1 
--SELECT  a.cdmcode, b.cdmdesc,b.ndc ,a.score
--     FROM combinedtable1 a, combinedtable1 b
--     WHERE a.score > b.score;

select Distinct d.cdmcode,d.cdmdesc, ds.ndc, ds.score 
 from combinedtable as d 
 cross apply 
     (select top 1 cdmcode,cdmdesc,score, ndc
      from combinedtable as d1 
      where d1.cdmcode = d.cdmcode
      order by d1.score desc) as ds
       