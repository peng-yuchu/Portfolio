--Triggers for tip counts
CREATE OR REPLACE FUNCTION business_tip_count()
RETURNS TRIGGER AS 
$BODY$
BEGIN
UPDATE Business
SET tip_count = tip_count + 1
WHERE Business.business_id = NEW.business_id;
RETURN NEW;
END;
$BODY$
LANGUAGE'plpgsql';

CREATE TRIGGER business_tip_count
AFTER INSERT
ON Tips
FOR EACH ROW 
EXECUTE PROCEDURE business_tip_count();


CREATE OR REPLACE FUNCTION users_tip_count()
RETURNS TRIGGER AS 
$BODY$
BEGIN
UPDATE Users
SET tip_count = tip_count + 1
WHERE Users.user_id = NEW.user_id;
RETURN NEW;
END;
$BODY$
LANGUAGE'plpgsql';

CREATE TRIGGER users_tip_count
AFTER INSERT
ON Tips
FOR EACH ROW 
EXECUTE PROCEDURE users_tip_count();
 
--SELECT tip_count FROM business WHERE business_id = 'gnKjwL_1w79qoiV3IC_xQQ';
--SELECT tip_count FROM users WHERE user_id = '4XChL029mKr5hydo79Ljxg';

--INSERT INTO Tips(user_id,business_id,date_created,like_count,tip_text) VALUES('4XChL029mKr5hydo79Ljxg','gnKjwL_1w79qoiV3IC_xQQ','2020-01-01',0,'test');

--SELECT tip_count FROM business WHERE business_id = 'gnKjwL_1w79qoiV3IC_xQQ';
--SELECT tip_count FROM users WHERE user_id = '4XChL029mKr5hydo79Ljxg';


CREATE OR REPLACE FUNCTION business_checkin_count()
RETURNS TRIGGER AS 
$BODY$
BEGIN
UPDATE Business
SET checkin_count = checkin_count + 1
WHERE Business.business_id = NEW.business_id;
RETURN NEW;
END;
$BODY$
LANGUAGE'plpgsql';

CREATE TRIGGER business_checkin_count
AFTER INSERT
ON Checksin
FOR EACH ROW 
EXECUTE PROCEDURE business_checkin_count();

--SELECT checkin_count FROM business WHERE business_id = 'gnKjwL_1w79qoiV3IC_xQQ';
--DELETE FROM Checksin WHERE checkin_time = 'test' AND business_id = 'gnKjwL_1w79qoiV3IC_xQQ';
--INSERT INTO Checksin(checkin_time,business_id) VALUES('test','gnKjwL_1w79qoiV3IC_xQQ');
--SELECT checkin_count FROM business WHERE business_id = 'gnKjwL_1w79qoiV3IC_xQQ';


CREATE OR REPLACE FUNCTION tip_like()
RETURNS TRIGGER AS 
$BODY$
BEGIN
UPDATE Users
SET tip_like_count = tip_like_count + 1
WHERE Users.user_id = NEW.user_id;
RETURN NEW;
END;
$BODY$
LANGUAGE'plpgsql';

CREATE TRIGGER tip_like
AFTER UPDATE
ON Tips
FOR EACH ROW 
EXECUTE PROCEDURE tip_like();

--SELECT like_count FROM Tips WHERE tip_id = 1;
--SELECT tip_like_count FROM Users WHERE user_id = 'jRyO2V1pA4CdVVqCIOPc1Q';

--UPDATE Tips
--SET like_count = like_count + 1
--WHERE Tips.tip_id = 1;

--SELECT like_count FROM Tips WHERE tip_id = 1;
--SELECT tip_like_count FROM Users WHERE user_id = 'jRyO2V1pA4CdVVqCIOPc1Q';














