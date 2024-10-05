#include<iostream>
#include<string>
#include<cstdlib>
#include<cctype>

typedef struct treenode *tree;
struct treenode {
    char info;
    tree left;
    tree right;
};

static int i = 0;
char nextsym(char input[]);
char next;
char input[10];

tree treebuild(char x, tree a, tree b);
tree proc_e();
tree proc_t();
tree proc_v();
void inorder(tree root);

int main() {
    tree root;
    int len;
    
    std::cout << "Enter an expression: ";
    std::cin >> input;

    len = strlen(input);
    next = nextsym(input);
    root = proc_e();
    
    if (len != i - 1) {
        std::cout << "Error" << std::endl;
        return 0;
    } else {
        std::cout << "It's a valid expression." << std::endl;
        inorder(root);
    }

    return 0;
}

tree treebuild(char x, tree a, tree b) {
    tree t = new treenode;
    t->info = x;
    t->left = a;
    t->right = b;
    return t;
}

tree proc_e() {
    tree a, b;
    a = proc_t();
    while (next == '+' || next == '-') {
        if (next == '+') {
            next = nextsym(input);
            b = proc_t();
            a = treebuild('+', a, b);
        } else {
            next = nextsym(input);
            b = proc_t();
            a = treebuild('-', a, b);
        }
    }
    return a;
}

tree proc_t() {
    tree a, b;
    a = proc_v();
    while (next == '*' || next == '/') {
        if (next == '*') {
            next = nextsym(input);
            b = proc_v();
            a = treebuild('*', a, b);
        } else {
            next = nextsym(input);
            b = proc_v();
            a = treebuild('/', a, b);  // corrected escape character issue
        }
    }
    return a;
}

tree proc_v() {
    tree a;
    if (isalpha(next)) {
        a = treebuild(next, nullptr, nullptr);
    } else {
        std::cout << "Error: Invalid character" << std::endl;
        exit(0);
    }
    next = nextsym(input);
    return a;
}

char nextsym(char input[]) {
    i++;
    return input[i - 1];
}

void inorder(tree t) {
    if (t != nullptr) {
        inorder(t->left);
        std::cout << t->info << " ";
        inorder(t->right);
    }
}
