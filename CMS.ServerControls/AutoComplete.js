var word = ''; 
var UP = 38; 
var DOWN = 40; 
var ENTER = 13;
var index = -1; 
var TAB = 9; 
var BACKSPACE = 8; 

var table = null; 
var rows = null;

var selectedRow = null;

function GetProducts(e) 
{
var keynum
var keychar
var numcheck

if(window.event) // IE
{
keynum = e.keyCode
}
else if(e.which) // Netscape/Firefox/Opera
{
keynum = e.which
}
keychar = String.fromCharCode(keynum)
numcheck = /\d/

// If the down key is pressed
if(keynum == DOWN) 
{   
    MoveCursorDown();    
    return;
}

else if(keynum == UP) 
{   
    MoveCursorUp();    
    return; 
}

else if(keynum == ENTER) 
{
    if(IsFireFox()) 
    {
    document.getElementById("txtSearch").value = selectedRow.childNodes[1].innerHTML;
    }
    else 
    {
        document.getElementById("txtSearch").value = selectedRow.innerText; 
    }
    document.getElementById("results").innerHTML = '';
    // false is returned so that the postback won't occur when the return key is pressed
    return false;
}

if(keynum != DOWN && keynum != UP && keynum >= 65 && keynum <= 90) 
{
    word = word + keychar; 
}

else if(keynum == BACKSPACE) 
{
    
    word = word.substring(0,word.length-1); 
}

// Call the server side method

CallServer(word,'');

}

function IsFireFox() 
{
    return (navigator.appName == 'Netscape'); 
}

function MoveCursorUp() 
{
    selectedRow = null; 
    table = document.getElementById("MyTable");
    
    if(table == null) return;
        
    rows = table.getElementsByTagName("TR");   
    
    if(index > 0) 
    {
      index--; 
      
      SetDefaultRowColor();
      selectedRow = rows[index];
      selectedRow.className = 'HighlightRow'      
    } 
}

function MoveCursorDown() 
{
    selectedRow = null; 
    table = document.getElementById("MyTable");
    
    if(table == null) return;
    
    rows = table.getElementsByTagName("TR");
        
    if(index < rows.length) 
    {   
              
        if(index < rows.length -1)
        { 
        index++;       
        SetDefaultRowColor();          
        selectedRow = rows[index];
        selectedRow.className = 'HighlightRow';   
        }
       
    } 
}

function SetDefaultRowColor() 
{
   for(i=0;i<rows.length;i++) 
    {
        rows[i].className = 'DefaultRowColor';
    }   
}


function RecieveServerData(response) 
{
    document.getElementById("results").innerHTML = response; 
}

