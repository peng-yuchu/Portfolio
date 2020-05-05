CREATE OR REPLACE FUNCTION distance(decimal, decimal, decimal, decimal) RETURNS double precision
AS 'SELECT 2*(6367.4445)*asin(sqrt(power(sin(($3 - $1)/2),2)+cos($1)*cos($3)*sin(power(($4-$2)/2,2))));'
LANGUAGE SQL
IMMUTABLE
RETURNS NULL ON NULL INPUT;
