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
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="HaCSParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.3")]
[System.CLSCompliant(false)]
public interface IHaCSVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by the <c>Or</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOr([NotNull] HaCSParser.OrContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Exponent</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExponent([NotNull] HaCSParser.ExponentContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Arith2</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArith2([NotNull] HaCSParser.Arith2Context context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Func</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunc([NotNull] HaCSParser.FuncContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Parens</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParens([NotNull] HaCSParser.ParensContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>And</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnd([NotNull] HaCSParser.AndContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Lit</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLit([NotNull] HaCSParser.LitContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Var</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVar([NotNull] HaCSParser.VarContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Arith1</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArith1([NotNull] HaCSParser.Arith1Context context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Compare</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompare([NotNull] HaCSParser.CompareContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Negate</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNegate([NotNull] HaCSParser.NegateContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Equality</c>
	/// labeled alternative in <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEquality([NotNull] HaCSParser.EqualityContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] HaCSParser.ProgramContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.main"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMain([NotNull] HaCSParser.MainContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.functionDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionDecl([NotNull] HaCSParser.FunctionDeclContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.formalParam"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFormalParam([NotNull] HaCSParser.FormalParamContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.body"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBody([NotNull] HaCSParser.BodyContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStmt([NotNull] HaCSParser.StmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.ifStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStmt([NotNull] HaCSParser.IfStmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.elseifStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseifStmt([NotNull] HaCSParser.ElseifStmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.elseStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseStmt([NotNull] HaCSParser.ElseStmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.varDcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarDcl([NotNull] HaCSParser.VarDclContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.listDcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListDcl([NotNull] HaCSParser.ListDclContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.returnStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnStmt([NotNull] HaCSParser.ReturnStmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] HaCSParser.ExpressionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitType([NotNull] HaCSParser.TypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.primitiveType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrimitiveType([NotNull] HaCSParser.PrimitiveTypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.listType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListType([NotNull] HaCSParser.ListTypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral([NotNull] HaCSParser.LiteralContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="HaCSParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompileUnit([NotNull] HaCSParser.CompileUnitContext context);
}
} // namespace HaCS
