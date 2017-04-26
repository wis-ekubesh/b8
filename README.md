# b8
b8 compiler from MMCS SFedU


Credits:
- Aliev M.
- Avakyan G.
- Bulanov D.
- Ermolaev D.
- Ermolinsky A.
- Fateev S.
- Bulanov D.
- Koval N.
- Limarev V.
- Nezhevsky N.
- Ostapenko A.
- Paterikin A.
- Raskin A.
- Rogov D.
- Cherkasov V.


| Team          | Dev1          | Dev2           | 
| ------------- |:-------------:| :-------------:| 
| RR            | Rogov D.      | Raskin A.      |
| AN            | Avakyan G     | Nezhevsky N.   |
| LK            | Limarev V.    | Koval N.       |
| FB            | Fateev S.     | Bulanov D.     |
| PC            | Paterikin A.  | Cherkasov V.   |
| OE            | Ostapenko A.  | Ermolaev D.    |
| AE            | Aliev M.      | Ermolinsky A.  |


Scientific Advisor: Mikhalkovich S.

Tasks:

RR  Rogov D.    Raskin A.
- Активные переменные внутри ББЛ
- Доступные выражения между базовыми блоками - множества e_gen(b) и e_kill(b)

AN  Avakyan G   Nezhevsky N.
- [x] Генерация трехадресного кода 
- [ ] Достигающие определения: вычисление gen_B и kill_B
- [ ] Построить на CFG глубинное остовное дерево, перенумеровав элементы в порядке обратном посфиксному

LK  Kubesh. E.  Koval N.
- [ ] Достигающие определения внутри ББЛ(+оптимизации)

FB  Fateev S.   Bulanov D.
- [ ] Активные переменные между ББЛ -- множества def_B и use_B
- [ ]  Достигающие выражения, итерационный алгоритм

PC  Paterikin A.    Cherkasov V.
- [x] Разбиение на базовые блоки
- [x] Создание CFG из базовых блоков 
- [ ] Итерационный алгоритм для достигающих определений
- [ ]  Дерево доминаторов 

OE  Ostapenko A.    Ermolaev D.
- [ ]  Классификация ребер графа: наступающие, отступающие, поперечные
- [ ]  Установить, все ли отступающие ребра обратные

AE  Aliev M.    Ermolinsky A.
- [ ]  Живые переменные - итерационный алгоритм

-------------------
- [ ]  Естественные циклы
- [ ]  goto

- [ ] 3 примера программ, дающих неправильные графы, найти отступающие ребра, не явл. обратными.
- [ ] оптимизация общего итерационного алгоритма обхода