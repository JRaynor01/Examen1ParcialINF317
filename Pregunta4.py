import multiprocessing

def generar_serie(posiciones):
    serie = [2]
    for i in range(1, posiciones):
        if i % 2 == 1:
            serie.append(i + 1)
        else:
            serie.append(serie[i - 1] + serie[i - 2] + 1)
    return serie

if __name__ == "__main__":
    num_posiciones = 10000

    # Crear un grupo de procesos
    with multiprocessing.Pool() as pool:
        serie_resultado = pool.map(generar_serie, [num_posiciones])

    # Obtener la serie generada
    serie_final = serie_resultado[0]

    # Imprimir los primeros 10 elementos de la serie
    print(serie_final)
