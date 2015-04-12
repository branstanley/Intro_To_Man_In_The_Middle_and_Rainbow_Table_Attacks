Create function new_user(theusername text, thepass text)
		Returns text
As $$

Begin

     insert into authen (username, pass) values (theusername, MD5(thepass));
	return MD5(thepass);
End;

$$language plpgsql;