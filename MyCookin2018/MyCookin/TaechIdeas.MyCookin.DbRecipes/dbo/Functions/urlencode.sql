CREATE function urlencode(@string varchar(4000))
RETURNS varchar(4000) AS BEGIN 
    declare @out varchar(4000)

    --convert to ASCII
    set @out = lower(@string COLLATE SQL_Latin1_General_CP1251_CS_AS)

    declare @pi int 
    --I'm sorry T-SQL have no regex. Thanks for patindex, MS .. :-)
    set @pi = patindex('%[^a-z0-9 -]%',@out)
    while @pi>0 begin
        set @out = replace(@out, substring(@out,@pi,1), '')
        --set @out = left(@out,@pi-1) + substring(@out,@pi+1,8000)
        set @pi = patindex('%[^a-z0-9 -]%',@out)
    end

    set @out = ltrim(rtrim(@out))

   -- replace space to hyphen   
   set @out = replace(@out, ' ', '-')

   -- remove double hyphen
   while CHARINDEX('--', @out) > 0 set @out = replace(@out, '--', '-')

   return (@out)
end
go