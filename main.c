/******************************************************************************

Welcome to GDB Online.
GDB online is an online compiler and debugger tool for C, C++, Python, Java, PHP, Ruby, Perl,
C#, OCaml, VB, Swift, Pascal, Fortran, Haskell, Objective-C, Assembly, HTML, CSS, JS, SQLite, Prolog.
Code, Compile, Run and Debug online from anywhere in world.

*******************************************************************************/
#include <stdio.h>
#include <ctype.h>
#include <stdlib.h>

enum token_type { T_PLUS, T_MINUS, T_MUL, T_DIV, T_NUM, T_LPAREN, T_RPAREN, T_EOF };

struct token {
    enum token_type type;
    int value; // For numbers, this will hold the integer value
};

// Global variable to store the current character
char *input;
int current_char;

// Function to get the next character
void next_char() {
    current_char = *input++;
}

// Function to create a token
struct token make_token(enum token_type type, int value) {
    struct token tok;
    tok.type = type;
    tok.value = value;
    return tok;
}

// Lexical Analyzer function
struct token lex() {
    while (isspace(current_char)) {
        next_char();
    }

    if (isdigit(current_char)) {
        int num = 0;
        while (isdigit(current_char)) {
            num = num * 10 + (current_char - '0');
            next_char();
        }
        return make_token(T_NUM, num);
    }

    if (current_char == '+') {
        next_char();
        return make_token(T_PLUS, 0);
    }

    if (current_char == '-') {
        next_char();
        return make_token(T_MINUS, 0);
    }

    if (current_char == '*') {
        next_char();
        return make_token(T_MUL, 0);
    }

    if (current_char == '/') {
        next_char();
        return make_token(T_DIV, 0);
    }

    if (current_char == '(') {
        next_char();
        return make_token(T_LPAREN, 0);
    }

    if (current_char == ')') {
        next_char();
        return make_token(T_RPAREN, 0);
    }

    if (current_char == '\0') {
        return make_token(T_EOF, 0);
    }

    printf("Unknown character: %c\n", current_char);
    exit(1);
}

// Parser and Evaluator
int expr();
int term();
int factor();

// Expression parsing (Handles + and -)
int expr() {
    int result = term();

    while (1) {
        struct token tok = lex();
        if (tok.type == T_PLUS) {
            result += term();
        } else if (tok.type == T_MINUS) {
            result -= term();
        } else {
            input--; // Put the token back for the next rule
            break;
        }
    }
    return result;
}

// Term parsing (Handles * and /)
int term() {
    int result = factor();

    while (1) {
        struct token tok = lex();
        if (tok.type == T_MUL) {
            result *= factor();
        } else if (tok.type == T_DIV) {
            result /= factor();
        } else {
            input--; // Put the token back for the next rule
            break;
        }
    }
    return result;
}

// Factor parsing (Handles numbers and parentheses)
int factor() {
    struct token tok = lex();
    int result;

    if (tok.type == T_NUM) {
        result = tok.value;
    } else if (tok.type == T_LPAREN) {
        result = expr();
        tok = lex(); // Consume closing parenthesis
        if (tok.type != T_RPAREN) {
            printf("Expected ')'\n");
            exit(1);
        }
    } else {
        printf("Unexpected token in factor\n");
        exit(1);
    }
    return result;
}

// Main driver function
int main() {
    char expression[] = "3 + 5 * (2 - 4)"; // Test input
    input = expression;
    next_char(); // Initialize the current character

    int result = expr();
    printf("Result: %d\n", result);

    return 0;
}




