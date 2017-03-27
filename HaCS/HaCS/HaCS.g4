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
 
 main : 'int' 'main' LPAREN formalParam(DELIMITER formalParam)* RPAREN body;

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

 expression : LPAREN expression RPAREN 
	|	expression ('++' | '--')
    |   NEGATE expression
	|   expression '^'<assoc = right> expression
    |   expression (MUL|DIV|MOD) expression
    |   expression (ADD|SUB) expression
    |   expression (LE | GE | GT | LT) expression
    |   expression (EQ | NEQ) expression
    |   expression AND expression
    |   expression OR expression
	|	funcCall								
	|	literal							    
	|	IDENTIFIER;						
			  

 funcCall : IDENTIFIER LPAREN IDENTIFIER (DELIMITER IDENTIFIER)* RPAREN;

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
