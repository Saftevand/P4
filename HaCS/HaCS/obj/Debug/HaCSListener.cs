//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Dank\Documents\P4\HaCS\HaCS\HaCS.g4 by ANTLR 4.5.3

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace HaCS {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="HaCSParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.3")]
[System.CLSCompliant(false)]
public interface IHaCSListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by the <c>Or</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOr([NotNull] HaCSParser.OrContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Or</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOr([NotNull] HaCSParser.OrContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Exponent</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExponent([NotNull] HaCSParser.ExponentContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Exponent</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExponent([NotNull] HaCSParser.ExponentContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Arith2</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArith2([NotNull] HaCSParser.Arith2Context context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Arith2</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArith2([NotNull] HaCSParser.Arith2Context context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Func</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunc([NotNull] HaCSParser.FuncContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Func</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunc([NotNull] HaCSParser.FuncContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Parens</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParens([NotNull] HaCSParser.ParensContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Parens</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParens([NotNull] HaCSParser.ParensContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Var</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVar([NotNull] HaCSParser.VarContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Var</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVar([NotNull] HaCSParser.VarContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Arith1</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArith1([NotNull] HaCSParser.Arith1Context context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Arith1</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArith1([NotNull] HaCSParser.Arith1Context context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Element</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterElement([NotNull] HaCSParser.ElementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Element</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitElement([NotNull] HaCSParser.ElementContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Range</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRange([NotNull] HaCSParser.RangeContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Range</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRange([NotNull] HaCSParser.RangeContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>And</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnd([NotNull] HaCSParser.AndContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>And</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnd([NotNull] HaCSParser.AndContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Lit</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLit([NotNull] HaCSParser.LitContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Lit</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLit([NotNull] HaCSParser.LitContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Compare</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCompare([NotNull] HaCSParser.CompareContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Compare</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCompare([NotNull] HaCSParser.CompareContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Negate</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNegate([NotNull] HaCSParser.NegateContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Negate</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNegate([NotNull] HaCSParser.NegateContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Equality</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEquality([NotNull] HaCSParser.EqualityContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Equality</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEquality([NotNull] HaCSParser.EqualityContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Lambda</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLambda([NotNull] HaCSParser.LambdaContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Lambda</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLambda([NotNull] HaCSParser.LambdaContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Last</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLast([NotNull] HaCSParser.LastContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Last</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLast([NotNull] HaCSParser.LastContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Exclude</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExclude([NotNull] HaCSParser.ExcludeContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Exclude</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExclude([NotNull] HaCSParser.ExcludeContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Length</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLength([NotNull] HaCSParser.LengthContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Length</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLength([NotNull] HaCSParser.LengthContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>IndexOf</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIndexOf([NotNull] HaCSParser.IndexOfContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>IndexOf</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIndexOf([NotNull] HaCSParser.IndexOfContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Find</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFind([NotNull] HaCSParser.FindContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Find</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFind([NotNull] HaCSParser.FindContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Contains</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterContains([NotNull] HaCSParser.ContainsContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Contains</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitContains([NotNull] HaCSParser.ContainsContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Include</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInclude([NotNull] HaCSParser.IncludeContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Include</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInclude([NotNull] HaCSParser.IncludeContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>First</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFirst([NotNull] HaCSParser.FirstContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>First</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFirst([NotNull] HaCSParser.FirstContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Where</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWhere([NotNull] HaCSParser.WhereContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Where</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWhere([NotNull] HaCSParser.WhereContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Map</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMap([NotNull] HaCSParser.MapContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Map</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMap([NotNull] HaCSParser.MapContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>Reduce</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReduce([NotNull] HaCSParser.ReduceContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Reduce</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReduce([NotNull] HaCSParser.ReduceContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>ExcludeAt</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExcludeAt([NotNull] HaCSParser.ExcludeAtContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ExcludeAt</c>
	/// labeled alternative in <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExcludeAt([NotNull] HaCSParser.ExcludeAtContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] HaCSParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] HaCSParser.ProgramContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.main"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMain([NotNull] HaCSParser.MainContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.main"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMain([NotNull] HaCSParser.MainContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.functionDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionDecl([NotNull] HaCSParser.FunctionDeclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.functionDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionDecl([NotNull] HaCSParser.FunctionDeclContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.formalParam"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFormalParam([NotNull] HaCSParser.FormalParamContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.formalParam"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFormalParam([NotNull] HaCSParser.FormalParamContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.body"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBody([NotNull] HaCSParser.BodyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.body"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBody([NotNull] HaCSParser.BodyContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStmt([NotNull] HaCSParser.StmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStmt([NotNull] HaCSParser.StmtContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.printStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrintStmt([NotNull] HaCSParser.PrintStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.printStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrintStmt([NotNull] HaCSParser.PrintStmtContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.ifStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfStmt([NotNull] HaCSParser.IfStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.ifStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfStmt([NotNull] HaCSParser.IfStmtContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.elseifStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterElseifStmt([NotNull] HaCSParser.ElseifStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.elseifStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitElseifStmt([NotNull] HaCSParser.ElseifStmtContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.elseStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterElseStmt([NotNull] HaCSParser.ElseStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.elseStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitElseStmt([NotNull] HaCSParser.ElseStmtContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.varDcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVarDcl([NotNull] HaCSParser.VarDclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.varDcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVarDcl([NotNull] HaCSParser.VarDclContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.listDcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterListDcl([NotNull] HaCSParser.ListDclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.listDcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitListDcl([NotNull] HaCSParser.ListDclContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.listDcls"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterListDcls([NotNull] HaCSParser.ListDclsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.listDcls"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitListDcls([NotNull] HaCSParser.ListDclsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.returnStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReturnStmt([NotNull] HaCSParser.ReturnStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.returnStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReturnStmt([NotNull] HaCSParser.ReturnStmtContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression([NotNull] HaCSParser.ExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression([NotNull] HaCSParser.ExpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.lambdaExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLambdaExp([NotNull] HaCSParser.LambdaExpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.lambdaExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLambdaExp([NotNull] HaCSParser.LambdaExpContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.lambdaBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLambdaBody([NotNull] HaCSParser.LambdaBodyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.lambdaBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLambdaBody([NotNull] HaCSParser.LambdaBodyContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterListOpp([NotNull] HaCSParser.ListOppContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.listOpp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitListOpp([NotNull] HaCSParser.ListOppContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType([NotNull] HaCSParser.TypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType([NotNull] HaCSParser.TypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.primitiveType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrimitiveType([NotNull] HaCSParser.PrimitiveTypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.primitiveType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrimitiveType([NotNull] HaCSParser.PrimitiveTypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.listType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterListType([NotNull] HaCSParser.ListTypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.listType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitListType([NotNull] HaCSParser.ListTypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HaCSParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCompileUnit([NotNull] HaCSParser.CompileUnitContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HaCSParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCompileUnit([NotNull] HaCSParser.CompileUnitContext context);
}
} // namespace HaCS
