<?php 
 $File = "./data/feature-requests.txt"; 
 $Handle = fopen($File, 'a');

 $Data = "Jane Doe\n"; 
 fwrite($Handle, $Data); 
 $Data = "Bilbo Jones\n"; 
 fwrite($Handle, $Data); 
 print "Data Written"; 
 fclose($Handle); 
 ?>