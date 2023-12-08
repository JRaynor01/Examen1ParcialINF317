#include <stdio.h>
#include <stdbool.h>
#include <omp.h>
#include <string.h>

int main() {
    const char *frase = "tres tristes tigres trigaban trigo por culpa del bolivar";
    char sub[100] = "";  
    char imp[100] = "";  
    char par[100] = "";  
    bool sw = true;

    #pragma omp parallel for private(sub) shared(frase, imp, par, sw)
    for (int i = 0; frase[i] != '\0'; i++) {
        if (frase[i] != ' ') {
            strncat(sub, &frase[i], 1);
        } else {
            if (sw) {
                #pragma omp critical
                {
                    strcat(imp, sub);
                    strcat(imp, " ");
                }
                sw = !sw;
            } else {
                #pragma omp critical
                {
                    strcat(par, sub);
                    strcat(par, " ");
                }
                sw = !sw;
            }
            sub[0] = '\0';  // Reiniciar subcadena
        }
    }

    if (sw) {
        #pragma omp critical
        {
            strcat(imp, sub);
        }
    } else {
        #pragma omp critical
        {
            strcat(par, sub);
        }
    }

    printf("Impares: %s\n", imp);
    printf("Pares: %s\n", par);

    return 0;
}
