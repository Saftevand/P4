grammar HaCS;


@parser::members
{
	protected const int EOF = Eof;
}

@lexer::members
{
	protected const int EOF = Eof;
	protected const int HIDDEN = Hidden;
}

/*
 * Parser Rules
 */
 program: main functionDecl*;
 
 main : INT_Type MAIN body;

 functionDecl : type IDENTIFIER LPAREN formalParam(DELIMITER formalParam)* RPAREN body;

 formalParam : type IDENTIFIER;
 
 body : LCURLBRACKET stmt* returnStmt RCURLBRACKET;

 stmt : ifStmt
	  | varDcl EOS
	  | printStmt EOS;

 printStmt : WRITELINE LPAREN expression (DELIMITER expression)* RPAREN; 

 ifStmt : IF LPAREN exp1=expression RPAREN body elseifStmt;

 elseifStmt : ELSEIF LPAREN exp2=expression RPAREN body elseifStmt
		    | elseStmt?;

 elseStmt : ELSE body;

 varDcl : left=primitiveType IDENTIFIER ASSIGN right=expression
		| listDcl;

listDcl : listType IDENTIFIER ASSIGN LCURLBRACKET listDcls RCURLBRACKET;
		
listDcls :  expression (DELIMITER expression)*
		 | LCURLBRACKET listDcls RCURLBRACKET (DELIMITER LCURLBRACKET listDcls RCURLBRACKET)* ;

 returnStmt :  RETURN expression EOS;

 expression : LPAREN expression RPAREN											#Parens
    |   NEGATE expression														#Negate
	|   left=expression EXP <assoc = right> right=expression					#Exponent
    |   left=expression (MUL|DIV|MOD) right=expression							#Arith2
    |   left=expression (ADD|SUB) right=expression								#Arith1
    |   left=expression (LE | GE | GT | LT) right=expression					#Compare
    |   left=expression (EQ | NEQ) right=expression								#Equality
    |	left=expression PIPE IDENTIFIER LTMINUS right=expression				#Pipe
	|   left=expression AND right=expression									#And
    |   left=expression OR right=expression										#Or
	|	IDENTIFIER LPAREN exp=expression (DELIMITER expList=expression)* RPAREN	#Func							
	|	(INT|FLOAT|CHAR|BOOL)													#Lit					    
	|	IDENTIFIER (DOT listOpp)?												#Var
	|	lambdaExp																#Lambda 
	|	left=expression DOT DOT right=expression								#Range
	|	IDENTIFIER LBRACKET expression RBRACKET									#Element;

 lambdaExp : LPAREN type IDENTIFIER (DELIMITER type IDENTIFIER)* RPAREN LAMBDA lambdaBody;

 lambdaBody : expression
			| body;

 listOpp : FIND LPAREN lambdaExp RPAREN								#Find
	|	WHERE LPAREN lambdaExp RPAREN								#Where
	|	FIRST LPAREN RPAREN											#First
	|	LAST LPAREN RPAREN											#Last
	|	MAP LPAREN lambdaExp RPAREN									#Map
	|	REDUCE LPAREN lambdaExp RPAREN								#Reduce
	|	CONTAINS LPAREN expression RPAREN							#Contains
	|	INCLUDE LPAREN expression (DELIMITER expression)* RPAREN	#Include
	|	EXCLUDE LPAREN expression (DELIMITER expression)* RPAREN	#Exclude
	|	EXCLUDEAT LPAREN expression (DELIMITER expression)* RPAREN	#ExcludeAt
	|	LENGTH LPAREN RPAREN										#Length
	|	FOLD LPAREN (ADD|SUB|MUL) RPAREN							#Fold
	|	INDEXOF LPAREN lambdaExp RPAREN								#IndexOf;

 type : primitiveType
	  | listType;

 primitiveType : INT_Type|CHAR_Type|FLOAT_Type|BOOL_Type;
 listType : LIST LT type GT;

compileUnit
	:	EOF;

/*
 * Lexer Rules
 */
INT : '-'?('0'..'9')+;
FLOAT : '-'?[0-9]+('.'[0-9]+)? ;
CHAR : '\'' ('\u0020'..'\u024F') '\'';
BOOL : ('true'|'false');
INT_Type : 'int';
FLOAT_Type : 'float';
CHAR_Type : 'char';
BOOL_Type : 'bool';
LIST : 'List';
MAIN : 'main';
IF : 'if';
ELSEIF : 'elseif';
ELSE : 'else';
RETURN : 'return';
FIND : 'find';
WHERE : 'where';
FIRST : 'first';
LAST : 'last';
MAP : 'map';
REDUCE : 'reduce';
FOLD : 'fold';
INDEXOF : 'indexOf';
CONTAINS: 'contains';
WRITELINE : 'WriteLine';
INCLUDE : 'include';
EXCLUDE : 'exclude';
EXCLUDEAT : 'excludeAt';
LENGTH : 'length';
IDENTIFIER : '_'?[a-zA-Z][a-zA-Z0-9]*;
EXP : '^';
MUL : '*';
DIV : '/';
MOD : '%';
ADD : '+';
SUB : '-';
AND : '&&';
OR	: '||';
PIPE: '|';
EQ	: '==';
NEQ	: '!=';
GT  : '>' ;
GE  : '>=' ;
LT  : '<' ;
DOT: '.';
LE  : '<=' ;
LTMINUS: '<-';
NEGATE: '!';
ASSIGN : '=';
LPAREN : '(' ;
RPAREN : ')' ;
LBRACKET : '[' ;
RBRACKET : ']' ;
LCURLBRACKET : '{' ;
RCURLBRACKET : '}' ;
DELIMITER : ',';
EOS : ';';
LAMBDA : '=>';


WS  :  [ \t\r\n\u000C]+ -> skip
    ;

COMMENT
    :   '/*' .*? '*/' -> channel(HIDDEN)
    ;

LINE_COMMENT
    :   '//' ~[\r\n]* -> channel(HIDDEN)
    ;
