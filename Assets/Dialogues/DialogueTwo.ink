VAR path1_done = false
VAR path2_done = false
VAR path3_done = false

VAR characterName = ""
VAR characterExpression = 1

VAR bgName = "black"
VAR alinaVisible = false
VAR ghostVisible = false

-> hub
=== hub ===
~bgName = "chapter2"

{ path1_done and path2_done and path3_done:
    -> chapter2_end
- else:
    Ты стоишь перед тремя коридорами. Какой выбрать?

    + {not path1_done} [Пойти в правую дверь]
        -> path1
    + {not path2_done} [Пойти в центральную дверь]
        -> path2
    + {not path3_done} [Пойти в левую дверь] 
        -> path3
}

=== path1 ===
~bgName = "black"
Я зашел в дверь
Везде темно
~bgName = "chapter2_class_room_normal"
Вдруг все появляется
Это мой класс..
~bgName = "chapter2_class_room_horror"
Все резко становится мрачным
Веет холодок
Появляется неизвестное существо...
~characterName = "???"
~ghostVisible = true
Ты же непомнишь да..
Как ты ее отверг...
А она ведь просила тебя
Умоляла
Только тебе доверяла
А ты испугался
    + [Я испугался...]
        -> yes
    + [Я не виноват, она странная]
        -> no
=== yes ===
~characterName = "Алина"
~characterExpression = 0
Ты все еще можешь спасти меня…
~ghostVisible = false
~characterName = ""
~ path1_done = true
-> return_to_hub

=== no ===
~characterExpression = 0
...
~ghostVisible = false
~characterName = ""
~ path1_done = true 
-> return_to_hub

=== path2 ===
#bg hallway_forward
Ты идёшь прямо, ощущая, как воздух становится плотнее
~ path2_done = true
-> return_to_hub

=== path3 ===
#bg hallway_right
Ты поворачиваешь направо. Тени ползут по стенам
~ path3_done = true
-> return_to_hub

=== return_to_hub ===
~bgName = "chapter2"
Ты возвращаешься на развилку...
-> hub

=== chapter2_end ===
#bg black
Все пути пройдены. Что-то изменилось внутри тебя.
-> END
