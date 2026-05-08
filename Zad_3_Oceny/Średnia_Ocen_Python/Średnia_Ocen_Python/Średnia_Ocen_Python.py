print("Program obliczajacy srednia ucznia")

liczbaOcen = int(input("Podaj liczbe ocen: "))

suma_ocen = 0

for i in range(liczbaOcen):
    ocena = float(input(f"Podaj ocene nr {i + 1}: "))
    suma_ocen = suma_ocen + ocena

    srednia = suma_ocen / liczbaOcen
    #Srednia z 1 miejscem dokladnosci po przecinku
print(f"Serdnia: {srednia:.1f}")

if srednia >= 3.0:
    print("Uczen zdal.")
else:
    print("Uczen nie zdal.")