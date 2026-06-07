print("Program kalkulatora")

liczba1 = int(input("Podaj pierwsza liczbe: "))
liczba2 = int(input("Podaj druga liczbe: "))
operator = input("Jakie dzialanie arytmetyczne chcesz wykonac? +, -, *, /: ")

if operator == "+":
    print(liczba1 + liczba2)
elif operator == "-":
    print(liczba1 - liczba2)
elif operator == "*":
    print(liczba1 * liczba2)
elif operator == "/":
    if liczba2 == 0:
        print("Nie wolno dzielic przez zero!")
    else:
        print(liczba1 / liczba2)
else:
    print("Wybrales zly operator/nie wybrano operatora!")