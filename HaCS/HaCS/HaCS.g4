grammar HaCS;


@parser::members
{
	protected const int EOF = Eof;
}

@lexer::memebers
{
	protected const int EOF = Eof;
	protected const int HIDDEN = Hidden;
}

/*
 * Parser Rules
 */
 program: main functionDecl*;
 
 main : 'int' 'main' body;

 functionDecl : type IDENTIFIER LPAREN formalParam(DELIMITER formalParam)* RPAREN body
			  | LPAREN formalParam(DELIMITER formalParam)* RPAREN LAMBDA body;

 formalParam : type IDENTIFIER;
 
 body : LCURLBRACKET stmt* RCURLBRACKET;

 stmt : ifStmt
	  | varDcl EOS
	  | returnStmt EOS;

 ifStmt : 'if' LPAREN expression RPAREN body elseifStmt;

 elseifStmt : 'elseif' LPAREN expression RPAREN body elseifStmt
		    | elseStmt?;

 elseStmt : 'else' body;

 varDcl : primitiveType IDENTIFIER ASSIGN expression
		| listType IDENTIFIER ASSIGN LCURLBRACKET expression (DELIMITER expression)* RCURLBRACKET;

 returnStmt : 'return' expression;

 expression : LPAREN expression RPAREN										#Parens
	|	expression ('++' | '--')											#IncDec
    |   NEGATE expression													#Negate
	|   expression '^'<assoc = right> expression							#Exponent
    |   expression (MUL|DIV|MOD) expression									#Arith2
    |   expression (ADD|SUB) expression										#Arith1
    |   expression (LE | GE | GT | LT) expression							#Compare
    |   expression (EQ | NEQ) expression									#Equality
    |   expression AND expression											#And
    |   expression OR expression											#Or
	|	IDENTIFIER LPAREN expression (DELIMITER expression)* RPAREN			#Func							
	|	literal																#Lit					    
	|	IDENTIFIER															#Var;						  

 type : primitiveType
	  | listType;

 primitiveType : 'int'|'char'|'float'|'bool';
 
 listType : 'List' LT type GT;

 literal : INT|FLOAT|CHAR|BOOL;

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
IDENTIFIER : '_'?[a-zA-Z][a-zA-Z0-9]*;
MUL : '*';
DIV : '/';
MOD : '%';
ADD : '+';
SUB : '-';
AND : '&&';
OR	: '||';
EQ	: '==';
NEQ	: '!=';
GT  : '>' ;
GE  : '>=' ;
LT  : '<' ;
LE  : '<=' ;
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
