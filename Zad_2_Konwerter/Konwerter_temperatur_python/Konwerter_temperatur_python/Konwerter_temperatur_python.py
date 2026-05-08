print("Konwerter temperatur C i F")

jednostka = input("Podaj jednostke: ")
temperatura = int(input("Podaj temperature: "))

temp_F = temperatura * 1.8 + 32
temp_C = (temperatura - 32) / 1.8

if jednostka == "C":
    print(f"Temperatura w stopniach Celsjusza wynosi: {temp_C}")
elif jednostka == "F":
    print(f"Temperatura w stopniach Farenheita wynosi: {temp_F}")