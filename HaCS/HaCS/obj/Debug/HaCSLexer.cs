//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\GryPetersen\Documents\P4\HaCS\HaCS\HaCS.g4 by ANTLR 4.5.3

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace HaCS {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.3")]
[System.CLSCompliant(false)]
public partial class HaCSLexer : Lexer {
	public const int
		INT=1, FLOAT=2, CHAR=3, BOOL=4, INT_Type=5, FLOAT_Type=6, CHAR_Type=7, 
		BOOL_Type=8, LIST=9, MAIN=10, IF=11, ELSEIF=12, ELSE=13, RETURN=14, FIND=15, 
		WHERE=16, FIRST=17, LAST=18, MAP=19, REDUCE=20, INDEXOF=21, CONTAINS=22, 
		WRITELINE=23, INCLUDE=24, EXCLUDE=25, EXCLUDEAT=26, LENGTH=27, IDENTIFIER=28, 
		EXP=29, MUL=30, DIV=31, MOD=32, ADD=33, SUB=34, AND=35, OR=36, EQ=37, 
		NEQ=38, GT=39, GE=40, LT=41, DOT=42, LE=43, LTMINUS=44, NEGATE=45, ASSIGN=46, 
		LPAREN=47, RPAREN=48, LBRACKET=49, RBRACKET=50, LCURLBRACKET=51, RCURLBRACKET=52, 
		DELIMITER=53, EOS=54, LAMBDA=55, WS=56, COMMENT=57, LINE_COMMENT=58;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"INT", "FLOAT", "CHAR", "BOOL", "INT_Type", "FLOAT_Type", "CHAR_Type", 
		"BOOL_Type", "LIST", "MAIN", "IF", "ELSEIF", "ELSE", "RETURN", "FIND", 
		"WHERE", "FIRST", "LAST", "MAP", "REDUCE", "INDEXOF", "CONTAINS", "WRITELINE", 
		"INCLUDE", "EXCLUDE", "EXCLUDEAT", "LENGTH", "IDENTIFIER", "EXP", "MUL", 
		"DIV", "MOD", "ADD", "SUB", "AND", "OR", "EQ", "NEQ", "GT", "GE", "LT", 
		"DOT", "LE", "LTMINUS", "NEGATE", "ASSIGN", "LPAREN", "RPAREN", "LBRACKET", 
		"RBRACKET", "LCURLBRACKET", "RCURLBRACKET", "DELIMITER", "EOS", "LAMBDA", 
		"WS", "COMMENT", "LINE_COMMENT"
	};


		protected const int EOF = Eof;
		protected const int HIDDEN = Hidden;


	public HaCSLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, null, null, "'int'", "'float'", "'char'", "'bool'", 
		"'List'", "'main'", "'if'", "'elseif'", "'else'", "'return'", "'find'", 
		"'where'", "'first'", "'last'", "'map'", "'reduce'", "'indexOf'", "'contains'", 
		"'WriteLine'", "'include'", "'exclude'", "'excludeAt'", "'length'", null, 
		"'^'", "'*'", "'/'", "'%'", "'+'", "'-'", "'&&'", "'||'", "'=='", "'!='", 
		"'>'", "'>='", "'<'", "'.'", "'<='", "'<-'", "'!'", "'='", "'('", "')'", 
		"'['", "']'", "'{'", "'}'", "','", "';'", "'=>'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "INT", "FLOAT", "CHAR", "BOOL", "INT_Type", "FLOAT_Type", "CHAR_Type", 
		"BOOL_Type", "LIST", "MAIN", "IF", "ELSEIF", "ELSE", "RETURN", "FIND", 
		"WHERE", "FIRST", "LAST", "MAP", "REDUCE", "INDEXOF", "CONTAINS", "WRITELINE", 
		"INCLUDE", "EXCLUDE", "EXCLUDEAT", "LENGTH", "IDENTIFIER", "EXP", "MUL", 
		"DIV", "MOD", "ADD", "SUB", "AND", "OR", "EQ", "NEQ", "GT", "GE", "LT", 
		"DOT", "LE", "LTMINUS", "NEGATE", "ASSIGN", "LPAREN", "RPAREN", "LBRACKET", 
		"RBRACKET", "LCURLBRACKET", "RCURLBRACKET", "DELIMITER", "EOS", "LAMBDA", 
		"WS", "COMMENT", "LINE_COMMENT"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[System.Obsolete("Use Vocabulary instead.")]
	public static readonly string[] tokenNames = GenerateTokenNames(DefaultVocabulary, _SymbolicNames.Length);

	private static string[] GenerateTokenNames(IVocabulary vocabulary, int length) {
		string[] tokenNames = new string[length];
		for (int i = 0; i < tokenNames.Length; i++) {
			tokenNames[i] = vocabulary.GetLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = vocabulary.GetSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}

		return tokenNames;
	}

	[System.Obsolete]
	public override string[] TokenNames
	{
		get
		{
			return tokenNames;
		}
	}

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "HaCS.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2<\x197\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t)\x4*\t"+
		"*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x4/\t/\x4\x30\t\x30\x4\x31\t\x31\x4\x32"+
		"\t\x32\x4\x33\t\x33\x4\x34\t\x34\x4\x35\t\x35\x4\x36\t\x36\x4\x37\t\x37"+
		"\x4\x38\t\x38\x4\x39\t\x39\x4:\t:\x4;\t;\x3\x2\x5\x2y\n\x2\x3\x2\x6\x2"+
		"|\n\x2\r\x2\xE\x2}\x3\x3\x5\x3\x81\n\x3\x3\x3\x6\x3\x84\n\x3\r\x3\xE\x3"+
		"\x85\x3\x3\x3\x3\x6\x3\x8A\n\x3\r\x3\xE\x3\x8B\x5\x3\x8E\n\x3\x3\x4\x3"+
		"\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5"+
		"\x5\x5\x9D\n\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a"+
		"\x3\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n"+
		"\x3\n\x3\v\x3\v\x3\v\x3\v\x3\v\x3\f\x3\f\x3\f\x3\r\x3\r\x3\r\x3\r\x3\r"+
		"\x3\r\x3\r\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF"+
		"\x3\xF\x3\xF\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x11\x3\x11\x3\x11\x3"+
		"\x11\x3\x11\x3\x11\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x13\x3"+
		"\x13\x3\x13\x3\x13\x3\x13\x3\x14\x3\x14\x3\x14\x3\x14\x3\x15\x3\x15\x3"+
		"\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3"+
		"\x16\x3\x16\x3\x16\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3"+
		"\x17\x3\x17\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3"+
		"\x18\x3\x18\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3"+
		"\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3"+
		"\x1B\x3\x1B\x3\x1B\x3\x1B\x3\x1B\x3\x1B\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3"+
		"\x1C\x3\x1C\x3\x1C\x3\x1C\x3\x1C\x3\x1D\x5\x1D\x131\n\x1D\x3\x1D\x3\x1D"+
		"\a\x1D\x135\n\x1D\f\x1D\xE\x1D\x138\v\x1D\x3\x1E\x3\x1E\x3\x1F\x3\x1F"+
		"\x3 \x3 \x3!\x3!\x3\"\x3\"\x3#\x3#\x3$\x3$\x3$\x3%\x3%\x3%\x3&\x3&\x3"+
		"&\x3\'\x3\'\x3\'\x3(\x3(\x3)\x3)\x3)\x3*\x3*\x3+\x3+\x3,\x3,\x3,\x3-\x3"+
		"-\x3-\x3.\x3.\x3/\x3/\x3\x30\x3\x30\x3\x31\x3\x31\x3\x32\x3\x32\x3\x33"+
		"\x3\x33\x3\x34\x3\x34\x3\x35\x3\x35\x3\x36\x3\x36\x3\x37\x3\x37\x3\x38"+
		"\x3\x38\x3\x38\x3\x39\x6\x39\x179\n\x39\r\x39\xE\x39\x17A\x3\x39\x3\x39"+
		"\x3:\x3:\x3:\x3:\a:\x183\n:\f:\xE:\x186\v:\x3:\x3:\x3:\x3:\x3:\x3;\x3"+
		";\x3;\x3;\a;\x191\n;\f;\xE;\x194\v;\x3;\x3;\x3\x184\x2\x2<\x3\x2\x3\x5"+
		"\x2\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2"+
		"\f\x17\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13"+
		"%\x2\x14\'\x2\x15)\x2\x16+\x2\x17-\x2\x18/\x2\x19\x31\x2\x1A\x33\x2\x1B"+
		"\x35\x2\x1C\x37\x2\x1D\x39\x2\x1E;\x2\x1F=\x2 ?\x2!\x41\x2\"\x43\x2#\x45"+
		"\x2$G\x2%I\x2&K\x2\'M\x2(O\x2)Q\x2*S\x2+U\x2,W\x2-Y\x2.[\x2/]\x2\x30_"+
		"\x2\x31\x61\x2\x32\x63\x2\x33\x65\x2\x34g\x2\x35i\x2\x36k\x2\x37m\x2\x38"+
		"o\x2\x39q\x2:s\x2;u\x2<\x3\x2\a\x3\x2\x32;\x4\x2\x43\\\x63|\x5\x2\x32"+
		";\x43\\\x63|\x5\x2\v\f\xE\xF\"\"\x4\x2\f\f\xF\xF\x1A2\x2\x3\x3\x2\x2\x2"+
		"\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2"+
		"\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2"+
		"\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3"+
		"\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3"+
		"\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2"+
		"\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2\x33\x3\x2\x2"+
		"\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2\x2;\x3\x2"+
		"\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2\x2\x2\x43\x3\x2"+
		"\x2\x2\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2\x2\x2K\x3\x2\x2"+
		"\x2\x2M\x3\x2\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2S\x3\x2\x2\x2\x2"+
		"U\x3\x2\x2\x2\x2W\x3\x2\x2\x2\x2Y\x3\x2\x2\x2\x2[\x3\x2\x2\x2\x2]\x3\x2"+
		"\x2\x2\x2_\x3\x2\x2\x2\x2\x61\x3\x2\x2\x2\x2\x63\x3\x2\x2\x2\x2\x65\x3"+
		"\x2\x2\x2\x2g\x3\x2\x2\x2\x2i\x3\x2\x2\x2\x2k\x3\x2\x2\x2\x2m\x3\x2\x2"+
		"\x2\x2o\x3\x2\x2\x2\x2q\x3\x2\x2\x2\x2s\x3\x2\x2\x2\x2u\x3\x2\x2\x2\x3"+
		"x\x3\x2\x2\x2\x5\x80\x3\x2\x2\x2\a\x8F\x3\x2\x2\x2\t\x9C\x3\x2\x2\x2\v"+
		"\x9E\x3\x2\x2\x2\r\xA2\x3\x2\x2\x2\xF\xA8\x3\x2\x2\x2\x11\xAD\x3\x2\x2"+
		"\x2\x13\xB2\x3\x2\x2\x2\x15\xB7\x3\x2\x2\x2\x17\xBC\x3\x2\x2\x2\x19\xBF"+
		"\x3\x2\x2\x2\x1B\xC6\x3\x2\x2\x2\x1D\xCB\x3\x2\x2\x2\x1F\xD2\x3\x2\x2"+
		"\x2!\xD7\x3\x2\x2\x2#\xDD\x3\x2\x2\x2%\xE3\x3\x2\x2\x2\'\xE8\x3\x2\x2"+
		"\x2)\xEC\x3\x2\x2\x2+\xF3\x3\x2\x2\x2-\xFB\x3\x2\x2\x2/\x104\x3\x2\x2"+
		"\x2\x31\x10E\x3\x2\x2\x2\x33\x116\x3\x2\x2\x2\x35\x11E\x3\x2\x2\x2\x37"+
		"\x128\x3\x2\x2\x2\x39\x130\x3\x2\x2\x2;\x139\x3\x2\x2\x2=\x13B\x3\x2\x2"+
		"\x2?\x13D\x3\x2\x2\x2\x41\x13F\x3\x2\x2\x2\x43\x141\x3\x2\x2\x2\x45\x143"+
		"\x3\x2\x2\x2G\x145\x3\x2\x2\x2I\x148\x3\x2\x2\x2K\x14B\x3\x2\x2\x2M\x14E"+
		"\x3\x2\x2\x2O\x151\x3\x2\x2\x2Q\x153\x3\x2\x2\x2S\x156\x3\x2\x2\x2U\x158"+
		"\x3\x2\x2\x2W\x15A\x3\x2\x2\x2Y\x15D\x3\x2\x2\x2[\x160\x3\x2\x2\x2]\x162"+
		"\x3\x2\x2\x2_\x164\x3\x2\x2\x2\x61\x166\x3\x2\x2\x2\x63\x168\x3\x2\x2"+
		"\x2\x65\x16A\x3\x2\x2\x2g\x16C\x3\x2\x2\x2i\x16E\x3\x2\x2\x2k\x170\x3"+
		"\x2\x2\x2m\x172\x3\x2\x2\x2o\x174\x3\x2\x2\x2q\x178\x3\x2\x2\x2s\x17E"+
		"\x3\x2\x2\x2u\x18C\x3\x2\x2\x2wy\a/\x2\x2xw\x3\x2\x2\x2xy\x3\x2\x2\x2"+
		"y{\x3\x2\x2\x2z|\x4\x32;\x2{z\x3\x2\x2\x2|}\x3\x2\x2\x2}{\x3\x2\x2\x2"+
		"}~\x3\x2\x2\x2~\x4\x3\x2\x2\x2\x7F\x81\a/\x2\x2\x80\x7F\x3\x2\x2\x2\x80"+
		"\x81\x3\x2\x2\x2\x81\x83\x3\x2\x2\x2\x82\x84\t\x2\x2\x2\x83\x82\x3\x2"+
		"\x2\x2\x84\x85\x3\x2\x2\x2\x85\x83\x3\x2\x2\x2\x85\x86\x3\x2\x2\x2\x86"+
		"\x8D\x3\x2\x2\x2\x87\x89\a\x30\x2\x2\x88\x8A\t\x2\x2\x2\x89\x88\x3\x2"+
		"\x2\x2\x8A\x8B\x3\x2\x2\x2\x8B\x89\x3\x2\x2\x2\x8B\x8C\x3\x2\x2\x2\x8C"+
		"\x8E\x3\x2\x2\x2\x8D\x87\x3\x2\x2\x2\x8D\x8E\x3\x2\x2\x2\x8E\x6\x3\x2"+
		"\x2\x2\x8F\x90\a)\x2\x2\x90\x91\x4\"\x251\x2\x91\x92\a)\x2\x2\x92\b\x3"+
		"\x2\x2\x2\x93\x94\av\x2\x2\x94\x95\at\x2\x2\x95\x96\aw\x2\x2\x96\x9D\a"+
		"g\x2\x2\x97\x98\ah\x2\x2\x98\x99\a\x63\x2\x2\x99\x9A\an\x2\x2\x9A\x9B"+
		"\au\x2\x2\x9B\x9D\ag\x2\x2\x9C\x93\x3\x2\x2\x2\x9C\x97\x3\x2\x2\x2\x9D"+
		"\n\x3\x2\x2\x2\x9E\x9F\ak\x2\x2\x9F\xA0\ap\x2\x2\xA0\xA1\av\x2\x2\xA1"+
		"\f\x3\x2\x2\x2\xA2\xA3\ah\x2\x2\xA3\xA4\an\x2\x2\xA4\xA5\aq\x2\x2\xA5"+
		"\xA6\a\x63\x2\x2\xA6\xA7\av\x2\x2\xA7\xE\x3\x2\x2\x2\xA8\xA9\a\x65\x2"+
		"\x2\xA9\xAA\aj\x2\x2\xAA\xAB\a\x63\x2\x2\xAB\xAC\at\x2\x2\xAC\x10\x3\x2"+
		"\x2\x2\xAD\xAE\a\x64\x2\x2\xAE\xAF\aq\x2\x2\xAF\xB0\aq\x2\x2\xB0\xB1\a"+
		"n\x2\x2\xB1\x12\x3\x2\x2\x2\xB2\xB3\aN\x2\x2\xB3\xB4\ak\x2\x2\xB4\xB5"+
		"\au\x2\x2\xB5\xB6\av\x2\x2\xB6\x14\x3\x2\x2\x2\xB7\xB8\ao\x2\x2\xB8\xB9"+
		"\a\x63\x2\x2\xB9\xBA\ak\x2\x2\xBA\xBB\ap\x2\x2\xBB\x16\x3\x2\x2\x2\xBC"+
		"\xBD\ak\x2\x2\xBD\xBE\ah\x2\x2\xBE\x18\x3\x2\x2\x2\xBF\xC0\ag\x2\x2\xC0"+
		"\xC1\an\x2\x2\xC1\xC2\au\x2\x2\xC2\xC3\ag\x2\x2\xC3\xC4\ak\x2\x2\xC4\xC5"+
		"\ah\x2\x2\xC5\x1A\x3\x2\x2\x2\xC6\xC7\ag\x2\x2\xC7\xC8\an\x2\x2\xC8\xC9"+
		"\au\x2\x2\xC9\xCA\ag\x2\x2\xCA\x1C\x3\x2\x2\x2\xCB\xCC\at\x2\x2\xCC\xCD"+
		"\ag\x2\x2\xCD\xCE\av\x2\x2\xCE\xCF\aw\x2\x2\xCF\xD0\at\x2\x2\xD0\xD1\a"+
		"p\x2\x2\xD1\x1E\x3\x2\x2\x2\xD2\xD3\ah\x2\x2\xD3\xD4\ak\x2\x2\xD4\xD5"+
		"\ap\x2\x2\xD5\xD6\a\x66\x2\x2\xD6 \x3\x2\x2\x2\xD7\xD8\ay\x2\x2\xD8\xD9"+
		"\aj\x2\x2\xD9\xDA\ag\x2\x2\xDA\xDB\at\x2\x2\xDB\xDC\ag\x2\x2\xDC\"\x3"+
		"\x2\x2\x2\xDD\xDE\ah\x2\x2\xDE\xDF\ak\x2\x2\xDF\xE0\at\x2\x2\xE0\xE1\a"+
		"u\x2\x2\xE1\xE2\av\x2\x2\xE2$\x3\x2\x2\x2\xE3\xE4\an\x2\x2\xE4\xE5\a\x63"+
		"\x2\x2\xE5\xE6\au\x2\x2\xE6\xE7\av\x2\x2\xE7&\x3\x2\x2\x2\xE8\xE9\ao\x2"+
		"\x2\xE9\xEA\a\x63\x2\x2\xEA\xEB\ar\x2\x2\xEB(\x3\x2\x2\x2\xEC\xED\at\x2"+
		"\x2\xED\xEE\ag\x2\x2\xEE\xEF\a\x66\x2\x2\xEF\xF0\aw\x2\x2\xF0\xF1\a\x65"+
		"\x2\x2\xF1\xF2\ag\x2\x2\xF2*\x3\x2\x2\x2\xF3\xF4\ak\x2\x2\xF4\xF5\ap\x2"+
		"\x2\xF5\xF6\a\x66\x2\x2\xF6\xF7\ag\x2\x2\xF7\xF8\az\x2\x2\xF8\xF9\aQ\x2"+
		"\x2\xF9\xFA\ah\x2\x2\xFA,\x3\x2\x2\x2\xFB\xFC\a\x65\x2\x2\xFC\xFD\aq\x2"+
		"\x2\xFD\xFE\ap\x2\x2\xFE\xFF\av\x2\x2\xFF\x100\a\x63\x2\x2\x100\x101\a"+
		"k\x2\x2\x101\x102\ap\x2\x2\x102\x103\au\x2\x2\x103.\x3\x2\x2\x2\x104\x105"+
		"\aY\x2\x2\x105\x106\at\x2\x2\x106\x107\ak\x2\x2\x107\x108\av\x2\x2\x108"+
		"\x109\ag\x2\x2\x109\x10A\aN\x2\x2\x10A\x10B\ak\x2\x2\x10B\x10C\ap\x2\x2"+
		"\x10C\x10D\ag\x2\x2\x10D\x30\x3\x2\x2\x2\x10E\x10F\ak\x2\x2\x10F\x110"+
		"\ap\x2\x2\x110\x111\a\x65\x2\x2\x111\x112\an\x2\x2\x112\x113\aw\x2\x2"+
		"\x113\x114\a\x66\x2\x2\x114\x115\ag\x2\x2\x115\x32\x3\x2\x2\x2\x116\x117"+
		"\ag\x2\x2\x117\x118\az\x2\x2\x118\x119\a\x65\x2\x2\x119\x11A\an\x2\x2"+
		"\x11A\x11B\aw\x2\x2\x11B\x11C\a\x66\x2\x2\x11C\x11D\ag\x2\x2\x11D\x34"+
		"\x3\x2\x2\x2\x11E\x11F\ag\x2\x2\x11F\x120\az\x2\x2\x120\x121\a\x65\x2"+
		"\x2\x121\x122\an\x2\x2\x122\x123\aw\x2\x2\x123\x124\a\x66\x2\x2\x124\x125"+
		"\ag\x2\x2\x125\x126\a\x43\x2\x2\x126\x127\av\x2\x2\x127\x36\x3\x2\x2\x2"+
		"\x128\x129\an\x2\x2\x129\x12A\ag\x2\x2\x12A\x12B\ap\x2\x2\x12B\x12C\a"+
		"i\x2\x2\x12C\x12D\av\x2\x2\x12D\x12E\aj\x2\x2\x12E\x38\x3\x2\x2\x2\x12F"+
		"\x131\a\x61\x2\x2\x130\x12F\x3\x2\x2\x2\x130\x131\x3\x2\x2\x2\x131\x132"+
		"\x3\x2\x2\x2\x132\x136\t\x3\x2\x2\x133\x135\t\x4\x2\x2\x134\x133\x3\x2"+
		"\x2\x2\x135\x138\x3\x2\x2\x2\x136\x134\x3\x2\x2\x2\x136\x137\x3\x2\x2"+
		"\x2\x137:\x3\x2\x2\x2\x138\x136\x3\x2\x2\x2\x139\x13A\a`\x2\x2\x13A<\x3"+
		"\x2\x2\x2\x13B\x13C\a,\x2\x2\x13C>\x3\x2\x2\x2\x13D\x13E\a\x31\x2\x2\x13E"+
		"@\x3\x2\x2\x2\x13F\x140\a\'\x2\x2\x140\x42\x3\x2\x2\x2\x141\x142\a-\x2"+
		"\x2\x142\x44\x3\x2\x2\x2\x143\x144\a/\x2\x2\x144\x46\x3\x2\x2\x2\x145"+
		"\x146\a(\x2\x2\x146\x147\a(\x2\x2\x147H\x3\x2\x2\x2\x148\x149\a~\x2\x2"+
		"\x149\x14A\a~\x2\x2\x14AJ\x3\x2\x2\x2\x14B\x14C\a?\x2\x2\x14C\x14D\a?"+
		"\x2\x2\x14DL\x3\x2\x2\x2\x14E\x14F\a#\x2\x2\x14F\x150\a?\x2\x2\x150N\x3"+
		"\x2\x2\x2\x151\x152\a@\x2\x2\x152P\x3\x2\x2\x2\x153\x154\a@\x2\x2\x154"+
		"\x155\a?\x2\x2\x155R\x3\x2\x2\x2\x156\x157\a>\x2\x2\x157T\x3\x2\x2\x2"+
		"\x158\x159\a\x30\x2\x2\x159V\x3\x2\x2\x2\x15A\x15B\a>\x2\x2\x15B\x15C"+
		"\a?\x2\x2\x15CX\x3\x2\x2\x2\x15D\x15E\a>\x2\x2\x15E\x15F\a/\x2\x2\x15F"+
		"Z\x3\x2\x2\x2\x160\x161\a#\x2\x2\x161\\\x3\x2\x2\x2\x162\x163\a?\x2\x2"+
		"\x163^\x3\x2\x2\x2\x164\x165\a*\x2\x2\x165`\x3\x2\x2\x2\x166\x167\a+\x2"+
		"\x2\x167\x62\x3\x2\x2\x2\x168\x169\a]\x2\x2\x169\x64\x3\x2\x2\x2\x16A"+
		"\x16B\a_\x2\x2\x16B\x66\x3\x2\x2\x2\x16C\x16D\a}\x2\x2\x16Dh\x3\x2\x2"+
		"\x2\x16E\x16F\a\x7F\x2\x2\x16Fj\x3\x2\x2\x2\x170\x171\a.\x2\x2\x171l\x3"+
		"\x2\x2\x2\x172\x173\a=\x2\x2\x173n\x3\x2\x2\x2\x174\x175\a?\x2\x2\x175"+
		"\x176\a@\x2\x2\x176p\x3\x2\x2\x2\x177\x179\t\x5\x2\x2\x178\x177\x3\x2"+
		"\x2\x2\x179\x17A\x3\x2\x2\x2\x17A\x178\x3\x2\x2\x2\x17A\x17B\x3\x2\x2"+
		"\x2\x17B\x17C\x3\x2\x2\x2\x17C\x17D\b\x39\x2\x2\x17Dr\x3\x2\x2\x2\x17E"+
		"\x17F\a\x31\x2\x2\x17F\x180\a,\x2\x2\x180\x184\x3\x2\x2\x2\x181\x183\v"+
		"\x2\x2\x2\x182\x181\x3\x2\x2\x2\x183\x186\x3\x2\x2\x2\x184\x185\x3\x2"+
		"\x2\x2\x184\x182\x3\x2\x2\x2\x185\x187\x3\x2\x2\x2\x186\x184\x3\x2\x2"+
		"\x2\x187\x188\a,\x2\x2\x188\x189\a\x31\x2\x2\x189\x18A\x3\x2\x2\x2\x18A"+
		"\x18B\b:\x3\x2\x18Bt\x3\x2\x2\x2\x18C\x18D\a\x31\x2\x2\x18D\x18E\a\x31"+
		"\x2\x2\x18E\x192\x3\x2\x2\x2\x18F\x191\n\x6\x2\x2\x190\x18F\x3\x2\x2\x2"+
		"\x191\x194\x3\x2\x2\x2\x192\x190\x3\x2\x2\x2\x192\x193\x3\x2\x2\x2\x193"+
		"\x195\x3\x2\x2\x2\x194\x192\x3\x2\x2\x2\x195\x196\b;\x3\x2\x196v\x3\x2"+
		"\x2\x2\xF\x2x}\x80\x85\x8B\x8D\x9C\x130\x136\x17A\x184\x192\x4\b\x2\x2"+
		"\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace HaCS
