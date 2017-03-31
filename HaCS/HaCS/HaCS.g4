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

 functionDecl : type IDENTIFIER LPAREN formalParam(DELIMITER formalParam)* RPAREN body
			  | LPAREN formalParam(DELIMITER formalParam)* RPAREN LAMBDA body;

 formalParam : type IDENTIFIER;
 
 body : LCURLBRACKET stmt* RCURLBRACKET;

 stmt : ifStmt
	  | varDcl EOS
	  | returnStmt EOS;

 ifStmt : IF LPAREN expression RPAREN body elseifStmt;

 elseifStmt : ELSEIF LPAREN expression RPAREN body elseifStmt
		    | elseStmt?;

 elseStmt : ELSE body;

 varDcl : primitiveType IDENTIFIER ASSIGN expression
		| listType IDENTIFIER ASSIGN listDcl;
listDcl : LCURLBRACKET listDcl (DELIMITER listDcl)* RCURLBRACKET
		| expression (DELIMITER expression)*;

 returnStmt :  RETURN expression;

 expression : LPAREN expression RPAREN										#Parens
    |   NEGATE expression													#Negate
	|   expression EXP <assoc = right> expression							#Exponent
    |   expression (MUL|DIV|MOD) expression									#Arith2
    |   expression (ADD|SUB) expression										#Arith1
    |   expression (LE | GE | GT | LT) expression							#Compare
    |   expression (EQ | NEQ) expression									#Equality
    |	expression PIPE IDENTIFIER LTMINUS expression						#Pipe
	|   expression AND expression											#And
    |   expression OR expression											#Or
	|	IDENTIFIER LPAREN expression (DELIMITER expression)* RPAREN			#Func							
	|	(INT|FLOAT|CHAR|BOOL)												#Lit					    
	|	IDENTIFIER listOpp?													#Var
	|	LBRACKET IDENTIFIER DOTDOT IDENTIFIER RBRACKET					    #Range;

 type : primitiveType
	  | listType;

 primitiveType : INT_Type|CHAR_Type|FLOAT_Type|BOOL_Type;

 listOpp  : FIND LPAREN expression? RPAREN;

 listType : LIST LT type GT;

compileUnit
	:	EOF
	;

/*
 * Lexer Rules
 */
INT : '-'?('0'..'9')+;
FLOAT : '-'?[0-9]+('.'[0-9]+)? ;
CHAR : [\u0032-\u00126];
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
DOTDOT: '..';
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
FIND : '.find';

WS  :  [ \t\r\n\u000C]+ -> skip
    ;

COMMENT
    :   '/*' .*? '*/' -> channel(HIDDEN)
    ;

LINE_COMMENT
    :   '//' ~[\r\n]* -> channel(HIDDEN)
    ;
