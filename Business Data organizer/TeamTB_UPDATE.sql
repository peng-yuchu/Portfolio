--Update checkin_count
WITH cc AS (
	SELECT DISTINCT business_id, COUNT(business_id) AS c_count FROM Checksin GROUP BY 1
)

UPDATE Business
SET checkin_count = c_count
FROM cc
WHERE Business.business_id = cc.business_id;

--Update Business tip_count
WITH tc AS (
	SELECT DISTINCT business_id, COUNT(business_id) AS t_count FROM Tips GROUP BY 1
)

UPDATE Business
SET tip_count = t_count
FROM tc
WHERE Business.business_id = tc.business_id;

--Update Users tip_count
WITH utc AS (
	SELECT DISTINCT user_id, COUNT(user_id) as ut_count FROM Tips GROUP BY 1
)

UPDATE Users
SET tip_count = ut_count
FROM utc
WHERE Users.user_id = utc.user_id;

--Update tip_like_count
WITH tl AS (
	SELECT DISTINCT user_id, SUM(like_count) AS tl_count FROM Tips GROUP BY 1
)

UPDATE Users
SET tip_like_count = tl_count
FROM tl
WHERE Users.user_id = tl.user_id;