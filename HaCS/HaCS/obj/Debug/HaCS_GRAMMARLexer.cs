//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from c:\users\grypetersen\documents\visual studio 2015\Projects\HaCS\HaCS\HaCS.g4 by ANTLR 4.5.3

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
public partial class HaCS_GRAMMARLexer : Lexer {
	public const int
		Program=1, Main=2, Function=3, Body=4, Stmt=5, Return=6, IfStmt=7, Variable=8, 
		Condition=9, VarDcl=10, Expression=11, FuncCall=12, Type=13, TYPE_LITERAL=14, 
		INT=15, FLOAT=16, CHAR=17, BOOL=18, IDENTIFIER=19, MUL=20, DIV=21, ADD=22, 
		SUB=23, AND=24, OR=25, EQ=26, NEQ=27, GT=28, GE=29, LT=30, LE=31, NEGATE=32, 
		ASSIGN=33, LPAREN=34, RPAREN=35, LBRACKET=36, RBRACKET=37, LCURLBRACKET=38, 
		RCURLBRACKET=39, DELIMITER=40, EOS=41, LAMBDA=42, WS=43, COMMENT=44, LINE_COMMENT=45;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"Program", "Main", "Function", "Body", "Stmt", "Return", "IfStmt", "Variable", 
		"Condition", "VarDcl", "Expression", "FuncCall", "Type", "TYPE_LITERAL", 
		"INT", "FLOAT", "CHAR", "BOOL", "IDENTIFIER", "MUL", "DIV", "ADD", "SUB", 
		"AND", "OR", "EQ", "NEQ", "GT", "GE", "LT", "LE", "NEGATE", "ASSIGN", 
		"LPAREN", "RPAREN", "LBRACKET", "RBRACKET", "LCURLBRACKET", "RCURLBRACKET", 
		"DELIMITER", "EOS", "LAMBDA", "WS", "COMMENT", "LINE_COMMENT"
	};


	public HaCS_GRAMMARLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, "'*'", "'/'", "'+'", "'-'", 
		"'&&'", "'||'", "'=='", "'!='", "'>'", "'>='", "'<'", "'<='", "'!'", "'='", 
		"'('", "')'", "'['", "']'", "'{'", "'}'", "','", "';'", "'=>'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "Program", "Main", "Function", "Body", "Stmt", "Return", "IfStmt", 
		"Variable", "Condition", "VarDcl", "Expression", "FuncCall", "Type", "TYPE_LITERAL", 
		"INT", "FLOAT", "CHAR", "BOOL", "IDENTIFIER", "MUL", "DIV", "ADD", "SUB", 
		"AND", "OR", "EQ", "NEQ", "GT", "GE", "LT", "LE", "NEGATE", "ASSIGN", 
		"LPAREN", "RPAREN", "LBRACKET", "RBRACKET", "LCURLBRACKET", "RCURLBRACKET", 
		"DELIMITER", "EOS", "LAMBDA", "WS", "COMMENT", "LINE_COMMENT"
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
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2/\x18A\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t)\x4*\t"+
		"*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x3\x2\x3\x2\a\x2`\n\x2\f\x2\xE\x2\x63\v"+
		"\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3"+
		"\x3\x3\x3\x3\a\x3r\n\x3\f\x3\xE\x3u\v\x3\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4"+
		"\x3\x4\x3\x4\a\x4~\n\x4\f\x4\xE\x4\x81\v\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3"+
		"\x4\a\x4\x88\n\x4\f\x4\xE\x4\x8B\v\x4\x3\x4\x3\x4\x3\x4\x3\x4\x5\x4\x91"+
		"\n\x4\x3\x5\x3\x5\a\x5\x95\n\x5\f\x5\xE\x5\x98\v\x5\x3\x5\x3\x5\x3\x5"+
		"\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x5\x6\xA4\n\x6\x3\a\x3\a\x3"+
		"\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x5"+
		"\a\xB6\n\a\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b"+
		"\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b"+
		"\x3\b\x3\b\x5\b\xD4\n\b\x3\t\x3\t\x3\t\x3\n\x3\n\x3\v\x3\v\x3\v\x3\v\x3"+
		"\v\x3\f\x3\f\x3\f\x5\f\xE3\n\f\x3\r\x3\r\x3\r\a\r\xE8\n\r\f\r\xE\r\xEB"+
		"\v\r\x3\r\x3\r\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3"+
		"\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x5\xE\xFF\n\xE\x3\xF\x3\xF\x3"+
		"\xF\x3\xF\x5\xF\x105\n\xF\x3\x10\x5\x10\x108\n\x10\x3\x10\x6\x10\x10B"+
		"\n\x10\r\x10\xE\x10\x10C\x3\x11\x5\x11\x110\n\x11\x3\x11\x6\x11\x113\n"+
		"\x11\r\x11\xE\x11\x114\x3\x11\x3\x11\x6\x11\x119\n\x11\r\x11\xE\x11\x11A"+
		"\x5\x11\x11D\n\x11\x3\x12\x3\x12\x3\x13\x3\x13\x3\x13\x3\x13\x3\x13\x3"+
		"\x13\x3\x13\x3\x13\x3\x13\x5\x13\x12A\n\x13\x3\x14\x5\x14\x12D\n\x14\x3"+
		"\x14\x3\x14\a\x14\x131\n\x14\f\x14\xE\x14\x134\v\x14\x3\x15\x3\x15\x3"+
		"\x16\x3\x16\x3\x17\x3\x17\x3\x18\x3\x18\x3\x19\x3\x19\x3\x19\x3\x1A\x3"+
		"\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3"+
		"\x1E\x3\x1E\x3\x1E\x3\x1F\x3\x1F\x3 \x3 \x3 \x3!\x3!\x3\"\x3\"\x3#\x3"+
		"#\x3$\x3$\x3%\x3%\x3&\x3&\x3\'\x3\'\x3(\x3(\x3)\x3)\x3*\x3*\x3+\x3+\x3"+
		"+\x3,\x6,\x16C\n,\r,\xE,\x16D\x3,\x3,\x3-\x3-\x3-\x3-\a-\x176\n-\f-\xE"+
		"-\x179\v-\x3-\x3-\x3-\x3-\x3-\x3.\x3.\x3.\x3.\a.\x184\n.\f.\xE.\x187\v"+
		".\x3.\x3.\x3\x177\x2\x2/\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2"+
		"\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B\x2\xF\x1D"+
		"\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16+\x2\x17-\x2"+
		"\x18/\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39\x2\x1E;\x2"+
		"\x1F=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'M\x2(O\x2)Q\x2"+
		"*S\x2+U\x2,W\x2-Y\x2.[\x2/\x3\x2\b\x3\x2\x32;\x3\x2\x38\x38\x4\x2\x43"+
		"\\\x63|\x5\x2\x32;\x43\\\x63|\x5\x2\v\f\xE\xF\"\"\x4\x2\f\f\xF\xF\x1A8"+
		"\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2"+
		"\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2"+
		"\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2"+
		"\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3"+
		"\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2"+
		"\x2\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2"+
		"\x2\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2"+
		"\x2\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2"+
		"\x2\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2"+
		"\x2\x2K\x3\x2\x2\x2\x2M\x3\x2\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2"+
		"S\x3\x2\x2\x2\x2U\x3\x2\x2\x2\x2W\x3\x2\x2\x2\x2Y\x3\x2\x2\x2\x2[\x3\x2"+
		"\x2\x2\x3]\x3\x2\x2\x2\x5\x66\x3\x2\x2\x2\a\x90\x3\x2\x2\x2\t\x92\x3\x2"+
		"\x2\x2\v\xA3\x3\x2\x2\x2\r\xB5\x3\x2\x2\x2\xF\xD3\x3\x2\x2\x2\x11\xD5"+
		"\x3\x2\x2\x2\x13\xD8\x3\x2\x2\x2\x15\xDA\x3\x2\x2\x2\x17\xE2\x3\x2\x2"+
		"\x2\x19\xE4\x3\x2\x2\x2\x1B\xFE\x3\x2\x2\x2\x1D\x104\x3\x2\x2\x2\x1F\x107"+
		"\x3\x2\x2\x2!\x10F\x3\x2\x2\x2#\x11E\x3\x2\x2\x2%\x129\x3\x2\x2\x2\'\x12C"+
		"\x3\x2\x2\x2)\x135\x3\x2\x2\x2+\x137\x3\x2\x2\x2-\x139\x3\x2\x2\x2/\x13B"+
		"\x3\x2\x2\x2\x31\x13D\x3\x2\x2\x2\x33\x140\x3\x2\x2\x2\x35\x143\x3\x2"+
		"\x2\x2\x37\x146\x3\x2\x2\x2\x39\x149\x3\x2\x2\x2;\x14B\x3\x2\x2\x2=\x14E"+
		"\x3\x2\x2\x2?\x150\x3\x2\x2\x2\x41\x153\x3\x2\x2\x2\x43\x155\x3\x2\x2"+
		"\x2\x45\x157\x3\x2\x2\x2G\x159\x3\x2\x2\x2I\x15B\x3\x2\x2\x2K\x15D\x3"+
		"\x2\x2\x2M\x15F\x3\x2\x2\x2O\x161\x3\x2\x2\x2Q\x163\x3\x2\x2\x2S\x165"+
		"\x3\x2\x2\x2U\x167\x3\x2\x2\x2W\x16B\x3\x2\x2\x2Y\x171\x3\x2\x2\x2[\x17F"+
		"\x3\x2\x2\x2]\x61\x5\x5\x3\x2^`\x5\a\x4\x2_^\x3\x2\x2\x2`\x63\x3\x2\x2"+
		"\x2\x61_\x3\x2\x2\x2\x61\x62\x3\x2\x2\x2\x62\x64\x3\x2\x2\x2\x63\x61\x3"+
		"\x2\x2\x2\x64\x65\a&\x2\x2\x65\x4\x3\x2\x2\x2\x66g\ak\x2\x2gh\ap\x2\x2"+
		"hi\av\x2\x2ij\x3\x2\x2\x2jk\ao\x2\x2kl\a\x63\x2\x2lm\ak\x2\x2mn\ap\x2"+
		"\x2no\x3\x2\x2\x2os\x5\x45#\x2pr\x5\x11\t\x2qp\x3\x2\x2\x2ru\x3\x2\x2"+
		"\x2sq\x3\x2\x2\x2st\x3\x2\x2\x2tv\x3\x2\x2\x2us\x3\x2\x2\x2vw\x5G$\x2"+
		"wx\x5\t\x5\x2x\x6\x3\x2\x2\x2yz\x5\x1B\xE\x2z{\x5\'\x14\x2{\x7F\x5\x45"+
		"#\x2|~\x5\x11\t\x2}|\x3\x2\x2\x2~\x81\x3\x2\x2\x2\x7F}\x3\x2\x2\x2\x7F"+
		"\x80\x3\x2\x2\x2\x80\x82\x3\x2\x2\x2\x81\x7F\x3\x2\x2\x2\x82\x83\x5G$"+
		"\x2\x83\x84\x5\t\x5\x2\x84\x91\x3\x2\x2\x2\x85\x89\x5\x45#\x2\x86\x88"+
		"\x5\x11\t\x2\x87\x86\x3\x2\x2\x2\x88\x8B\x3\x2\x2\x2\x89\x87\x3\x2\x2"+
		"\x2\x89\x8A\x3\x2\x2\x2\x8A\x8C\x3\x2\x2\x2\x8B\x89\x3\x2\x2\x2\x8C\x8D"+
		"\x5G$\x2\x8D\x8E\x5U+\x2\x8E\x8F\x5\t\x5\x2\x8F\x91\x3\x2\x2\x2\x90y\x3"+
		"\x2\x2\x2\x90\x85\x3\x2\x2\x2\x91\b\x3\x2\x2\x2\x92\x96\x5M\'\x2\x93\x95"+
		"\x5\v\x6\x2\x94\x93\x3\x2\x2\x2\x95\x98\x3\x2\x2\x2\x96\x94\x3\x2\x2\x2"+
		"\x96\x97\x3\x2\x2\x2\x97\x99\x3\x2\x2\x2\x98\x96\x3\x2\x2\x2\x99\x9A\x5"+
		"\r\a\x2\x9A\x9B\x5O(\x2\x9B\n\x3\x2\x2\x2\x9C\xA4\x5\xF\b\x2\x9D\x9E\x5"+
		"\x15\v\x2\x9E\x9F\x5S*\x2\x9F\xA4\x3\x2\x2\x2\xA0\xA1\x5\r\a\x2\xA1\xA2"+
		"\x5S*\x2\xA2\xA4\x3\x2\x2\x2\xA3\x9C\x3\x2\x2\x2\xA3\x9D\x3\x2\x2\x2\xA3"+
		"\xA0\x3\x2\x2\x2\xA4\f\x3\x2\x2\x2\xA5\xA6\at\x2\x2\xA6\xA7\ag\x2\x2\xA7"+
		"\xA8\av\x2\x2\xA8\xA9\aw\x2\x2\xA9\xAA\at\x2\x2\xAA\xAB\ap\x2\x2\xAB\xAC"+
		"\x3\x2\x2\x2\xAC\xB6\x5\'\x14\x2\xAD\xAE\at\x2\x2\xAE\xAF\ag\x2\x2\xAF"+
		"\xB0\av\x2\x2\xB0\xB1\aw\x2\x2\xB1\xB2\at\x2\x2\xB2\xB3\ap\x2\x2\xB3\xB4"+
		"\x3\x2\x2\x2\xB4\xB6\x5\x1B\xE\x2\xB5\xA5\x3\x2\x2\x2\xB5\xAD\x3\x2\x2"+
		"\x2\xB6\xE\x3\x2\x2\x2\xB7\xB8\ak\x2\x2\xB8\xB9\ah\x2\x2\xB9\xBA\x3\x2"+
		"\x2\x2\xBA\xBB\x5\x45#\x2\xBB\xBC\x5\x13\n\x2\xBC\xBD\x5G$\x2\xBD\xBE"+
		"\x5\t\x5\x2\xBE\xBF\ag\x2\x2\xBF\xC0\an\x2\x2\xC0\xC1\au\x2\x2\xC1\xC2"+
		"\ag\x2\x2\xC2\xC3\x3\x2\x2\x2\xC3\xC4\x5\t\x5\x2\xC4\xD4\x3\x2\x2\x2\xC5"+
		"\xC6\ak\x2\x2\xC6\xC7\ah\x2\x2\xC7\xC8\x3\x2\x2\x2\xC8\xC9\x5\x45#\x2"+
		"\xC9\xCA\x5\x13\n\x2\xCA\xCB\x5G$\x2\xCB\xCC\x5\t\x5\x2\xCC\xCD\ag\x2"+
		"\x2\xCD\xCE\an\x2\x2\xCE\xCF\au\x2\x2\xCF\xD0\ag\x2\x2\xD0\xD1\x3\x2\x2"+
		"\x2\xD1\xD2\x5\xF\b\x2\xD2\xD4\x3\x2\x2\x2\xD3\xB7\x3\x2\x2\x2\xD3\xC5"+
		"\x3\x2\x2\x2\xD4\x10\x3\x2\x2\x2\xD5\xD6\x5\x1B\xE\x2\xD6\xD7\x5\'\x14"+
		"\x2\xD7\x12\x3\x2\x2\x2\xD8\xD9\x5%\x13\x2\xD9\x14\x3\x2\x2\x2\xDA\xDB"+
		"\x5\x1B\xE\x2\xDB\xDC\x5\'\x14\x2\xDC\xDD\x5\x43\"\x2\xDD\xDE\x5\x17\f"+
		"\x2\xDE\x16\x3\x2\x2\x2\xDF\xE3\x5\x19\r\x2\xE0\xE3\x5\x1D\xF\x2\xE1\xE3"+
		"\x5\'\x14\x2\xE2\xDF\x3\x2\x2\x2\xE2\xE0\x3\x2\x2\x2\xE2\xE1\x3\x2\x2"+
		"\x2\xE3\x18\x3\x2\x2\x2\xE4\xE5\x5\'\x14\x2\xE5\xE9\x5\x45#\x2\xE6\xE8"+
		"\x5\'\x14\x2\xE7\xE6\x3\x2\x2\x2\xE8\xEB\x3\x2\x2\x2\xE9\xE7\x3\x2\x2"+
		"\x2\xE9\xEA\x3\x2\x2\x2\xEA\xEC\x3\x2\x2\x2\xEB\xE9\x3\x2\x2\x2\xEC\xED"+
		"\x5G$\x2\xED\x1A\x3\x2\x2\x2\xEE\xEF\ak\x2\x2\xEF\xF0\ap\x2\x2\xF0\xFF"+
		"\av\x2\x2\xF1\xF2\a\x65\x2\x2\xF2\xF3\aj\x2\x2\xF3\xF4\a\x63\x2\x2\xF4"+
		"\xFF\at\x2\x2\xF5\xF6\ah\x2\x2\xF6\xF7\an\x2\x2\xF7\xF8\aq\x2\x2\xF8\xF9"+
		"\a\x63\x2\x2\xF9\xFF\av\x2\x2\xFA\xFB\a\x64\x2\x2\xFB\xFC\aq\x2\x2\xFC"+
		"\xFD\aq\x2\x2\xFD\xFF\an\x2\x2\xFE\xEE\x3\x2\x2\x2\xFE\xF1\x3\x2\x2\x2"+
		"\xFE\xF5\x3\x2\x2\x2\xFE\xFA\x3\x2\x2\x2\xFF\x1C\x3\x2\x2\x2\x100\x105"+
		"\x5\x1F\x10\x2\x101\x105\x5!\x11\x2\x102\x105\x5#\x12\x2\x103\x105\x5"+
		"%\x13\x2\x104\x100\x3\x2\x2\x2\x104\x101\x3\x2\x2\x2\x104\x102\x3\x2\x2"+
		"\x2\x104\x103\x3\x2\x2\x2\x105\x1E\x3\x2\x2\x2\x106\x108\a/\x2\x2\x107"+
		"\x106\x3\x2\x2\x2\x107\x108\x3\x2\x2\x2\x108\x10A\x3\x2\x2\x2\x109\x10B"+
		"\x4\x32;\x2\x10A\x109\x3\x2\x2\x2\x10B\x10C\x3\x2\x2\x2\x10C\x10A\x3\x2"+
		"\x2\x2\x10C\x10D\x3\x2\x2\x2\x10D \x3\x2\x2\x2\x10E\x110\a/\x2\x2\x10F"+
		"\x10E\x3\x2\x2\x2\x10F\x110\x3\x2\x2\x2\x110\x112\x3\x2\x2\x2\x111\x113"+
		"\t\x2\x2\x2\x112\x111\x3\x2\x2\x2\x113\x114\x3\x2\x2\x2\x114\x112\x3\x2"+
		"\x2\x2\x114\x115\x3\x2\x2\x2\x115\x11C\x3\x2\x2\x2\x116\x118\a\x30\x2"+
		"\x2\x117\x119\t\x2\x2\x2\x118\x117\x3\x2\x2\x2\x119\x11A\x3\x2\x2\x2\x11A"+
		"\x118\x3\x2\x2\x2\x11A\x11B\x3\x2\x2\x2\x11B\x11D\x3\x2\x2\x2\x11C\x116"+
		"\x3\x2\x2\x2\x11C\x11D\x3\x2\x2\x2\x11D\"\x3\x2\x2\x2\x11E\x11F\t\x3\x2"+
		"\x2\x11F$\x3\x2\x2\x2\x120\x121\av\x2\x2\x121\x122\at\x2\x2\x122\x123"+
		"\aw\x2\x2\x123\x12A\ag\x2\x2\x124\x125\ah\x2\x2\x125\x126\a\x63\x2\x2"+
		"\x126\x127\an\x2\x2\x127\x128\au\x2\x2\x128\x12A\ag\x2\x2\x129\x120\x3"+
		"\x2\x2\x2\x129\x124\x3\x2\x2\x2\x12A&\x3\x2\x2\x2\x12B\x12D\a\x61\x2\x2"+
		"\x12C\x12B\x3\x2\x2\x2\x12C\x12D\x3\x2\x2\x2\x12D\x12E\x3\x2\x2\x2\x12E"+
		"\x132\t\x4\x2\x2\x12F\x131\t\x5\x2\x2\x130\x12F\x3\x2\x2\x2\x131\x134"+
		"\x3\x2\x2\x2\x132\x130\x3\x2\x2\x2\x132\x133\x3\x2\x2\x2\x133(\x3\x2\x2"+
		"\x2\x134\x132\x3\x2\x2\x2\x135\x136\a,\x2\x2\x136*\x3\x2\x2\x2\x137\x138"+
		"\a\x31\x2\x2\x138,\x3\x2\x2\x2\x139\x13A\a-\x2\x2\x13A.\x3\x2\x2\x2\x13B"+
		"\x13C\a/\x2\x2\x13C\x30\x3\x2\x2\x2\x13D\x13E\a(\x2\x2\x13E\x13F\a(\x2"+
		"\x2\x13F\x32\x3\x2\x2\x2\x140\x141\a~\x2\x2\x141\x142\a~\x2\x2\x142\x34"+
		"\x3\x2\x2\x2\x143\x144\a?\x2\x2\x144\x145\a?\x2\x2\x145\x36\x3\x2\x2\x2"+
		"\x146\x147\a#\x2\x2\x147\x148\a?\x2\x2\x148\x38\x3\x2\x2\x2\x149\x14A"+
		"\a@\x2\x2\x14A:\x3\x2\x2\x2\x14B\x14C\a@\x2\x2\x14C\x14D\a?\x2\x2\x14D"+
		"<\x3\x2\x2\x2\x14E\x14F\a>\x2\x2\x14F>\x3\x2\x2\x2\x150\x151\a>\x2\x2"+
		"\x151\x152\a?\x2\x2\x152@\x3\x2\x2\x2\x153\x154\a#\x2\x2\x154\x42\x3\x2"+
		"\x2\x2\x155\x156\a?\x2\x2\x156\x44\x3\x2\x2\x2\x157\x158\a*\x2\x2\x158"+
		"\x46\x3\x2\x2\x2\x159\x15A\a+\x2\x2\x15AH\x3\x2\x2\x2\x15B\x15C\a]\x2"+
		"\x2\x15CJ\x3\x2\x2\x2\x15D\x15E\a_\x2\x2\x15EL\x3\x2\x2\x2\x15F\x160\a"+
		"}\x2\x2\x160N\x3\x2\x2\x2\x161\x162\a\x7F\x2\x2\x162P\x3\x2\x2\x2\x163"+
		"\x164\a.\x2\x2\x164R\x3\x2\x2\x2\x165\x166\a=\x2\x2\x166T\x3\x2\x2\x2"+
		"\x167\x168\a?\x2\x2\x168\x169\a@\x2\x2\x169V\x3\x2\x2\x2\x16A\x16C\t\x6"+
		"\x2\x2\x16B\x16A\x3\x2\x2\x2\x16C\x16D\x3\x2\x2\x2\x16D\x16B\x3\x2\x2"+
		"\x2\x16D\x16E\x3\x2\x2\x2\x16E\x16F\x3\x2\x2\x2\x16F\x170\b,\x2\x2\x170"+
		"X\x3\x2\x2\x2\x171\x172\a\x31\x2\x2\x172\x173\a,\x2\x2\x173\x177\x3\x2"+
		"\x2\x2\x174\x176\v\x2\x2\x2\x175\x174\x3\x2\x2\x2\x176\x179\x3\x2\x2\x2"+
		"\x177\x178\x3\x2\x2\x2\x177\x175\x3\x2\x2\x2\x178\x17A\x3\x2\x2\x2\x179"+
		"\x177\x3\x2\x2\x2\x17A\x17B\a,\x2\x2\x17B\x17C\a\x31\x2\x2\x17C\x17D\x3"+
		"\x2\x2\x2\x17D\x17E\b-\x3\x2\x17EZ\x3\x2\x2\x2\x17F\x180\a\x31\x2\x2\x180"+
		"\x181\a\x31\x2\x2\x181\x185\x3\x2\x2\x2\x182\x184\n\a\x2\x2\x183\x182"+
		"\x3\x2\x2\x2\x184\x187\x3\x2\x2\x2\x185\x183\x3\x2\x2\x2\x185\x186\x3"+
		"\x2\x2\x2\x186\x188\x3\x2\x2\x2\x187\x185\x3\x2\x2\x2\x188\x189\b.\x3"+
		"\x2\x189\\\x3\x2\x2\x2\x1C\x2\x61s\x7F\x89\x90\x96\xA3\xB5\xD3\xE2\xE9"+
		"\xFE\x104\x107\x10C\x10F\x114\x11A\x11C\x129\x12C\x132\x16D\x177\x185"+
		"\x4\b\x2\x2\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace HaCS
