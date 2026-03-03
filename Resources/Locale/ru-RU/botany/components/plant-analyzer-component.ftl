plant-analyzer-component-no-seed = растение не обнаружено

plant-analyzer-component-health = Здоровье:
plant-analyzer-component-age = Возраст:
plant-analyzer-component-water = Вода:
plant-analyzer-component-nutrition = Питание:
plant-analyzer-component-toxins = Токсины:
plant-analyzer-component-pests = Вредители:
plant-analyzer-component-weeds = Сорняки:

plant-analyzer-component-alive = [color=green]ЖИВОЕ[/color]
plant-analyzer-component-dead = [color=red]МЁРТВОЕ[/color]
plant-analyzer-component-unviable = [color=red]НЕЖИЗНЕСПОСОБНОЕ[/color]
plant-analyzer-component-mutating = [color=#00ff5f]МУТИРУЕТ[/color]
plant-analyzer-component-kudzu = [color=red]КУДЗУ[/color]

plant-analyzer-soil = В этом {$holder} находится [color=white]{$chemicals}[/color], {$count ->
    [one]который не был поглощён
    *[other]которые не были поглощены
}.
plant-analyzer-soil-empty = В этом {$holder} нет непоглощённых химикатов.

plant-analyzer-component-environemt = Это растение [color=green]{$seedName}[/color] требует атмосферу с уровнем давления [color=lightblue]{$kpa}кПа ± {$kpaTolerance}кПа[/color], температуру [color=lightsalmon]{$temp}°К ± {$tempTolerance}°К[/color] и уровень освещения [color=white]{$lightLevel} ± {$lightTolerance}[/color].
plant-analyzer-component-environemt-void = Это растение [color=green]{$seedName}[/color] должно выращиваться [bolditalic]в вакууме космоса[/bolditalic] при уровне освещения [color=white]{$lightLevel} ± {$lightTolerance}[/color].
plant-analyzer-component-environemt-gas = Это растение [color=green]{$seedName}[/color] требует атмосферу, содержащую [bold]{$gases}[/bold], с уровнем давления [color=lightblue]{$kpa}кПа ± {$kpaTolerance}кПа[/color], температуру [color=lightsalmon]{$temp}°К ± {$tempTolerance}°К[/color] и уровень освещения [color=white]{$lightLevel} ± {$lightTolerance}[/color].

plant-analyzer-produce-plural = {$thing}
plant-analyzer-output = {$yield ->
    [0]{$gasCount ->
        [0]Единственное, что оно делает - потребляет воду и питательные вещества.
        *[other]Единственное, что оно делает - превращает воду и питательные вещества в [bold]{$gases}[/bold].
    }
    *[other]Имеет [color=lightgreen]{$yield} {$potency}[/color]{$seedless ->
        [true]{" "}но [color=red]бессемянное[/color]
        *[false]{$nothing}
    }{" "}{$yield ->
        [one]цветок
        [2]цветка
        [3]цветка
        [4]цветка
        *[other]цветков
    }, {$yield ->
        [one]который
        *[other]которые
    }{$gasCount ->
        [0]{$nothing}
        *[other]{" "}{$yield ->
            [one]выделяет
            *[other]выделяют
        }{" "}[bold]{$gases}[/bold] и
    }{" "}{$yield ->
        [one]превратится
        *[other]превратятся
    } в [color=#a4885c]{$producePlural}[/color].{$chemCount ->
        [0]{$nothing}
        *[other]{" "}В стебле присутствуют следы [color=white]{$chemicals}[/color].
    }
}

plant-analyzer-potency-tiny = крошечные
plant-analyzer-potency-small = маленькие
plant-analyzer-potency-below-average = ниже среднего размера
plant-analyzer-potency-average = среднего размера
plant-analyzer-potency-above-average = выше среднего размера
plant-analyzer-potency-large = довольно крупные
plant-analyzer-potency-huge = огромные
plant-analyzer-potency-gigantic = гигантские
plant-analyzer-potency-ludicrous = невероятно большие
plant-analyzer-potency-immeasurable = неизмеримо большие

plant-analyzer-print = Печать
plant-analyzer-printout-missing = Н/Д
plant-analyzer-printout = [color=#9FED58][head=2]Отчёт анализатора растений[/head][/color]{$nl
    }──────────────────────────────{$nl
    }[bullet/] Вид: {$seedName}{$nl
    }{$indent}[bullet/] Жизнеспособность: {$viable ->
        [no][color=red]Нет[/color]
        [yes][color=green]Да[/color]
        *[other]{LOC("plant-analyzer-printout-missing")}
    }{$nl
    }{$indent}[bullet/] Выносливость: {$endurance}{$nl
    }{$indent}[bullet/] Продолжительность жизни: {$lifespan}{$nl
    }{$indent}[bullet/] Продукция: [color=#a4885c]{$produce}[/color]{$nl
    }{$indent}[bullet/] Кудзу: {$kudzu ->
        [no][color=green]Нет[/color]
        [yes][color=red]Да[/color]
        *[other]{LOC("plant-analyzer-printout-missing")}
    }{$nl
    }[bullet/] Профиль роста:{$nl
    }{$indent}[bullet/] Вода: [color=cyan]{$water}[/color]{$nl
    }{$indent}[bullet/] Питание: [color=orange]{$nutrients}[/color]{$nl
    }{$indent}[bullet/] Токсины: [color=yellowgreen]{$toxins}[/color]{$nl
    }{$indent}[bullet/] Вредители: [color=magenta]{$pests}[/color]{$nl
    }{$indent}[bullet/] Сорняки: [color=red]{$weeds}[/color]{$nl
    }[bullet/] Профиль окружающей среды:{$nl
    }{$indent}[bullet/] Состав: [bold]{$gasesIn}[/bold]{$nl
    }{$indent}[bullet/] Давление: [color=lightblue]{$kpa}кПа ± {$kpaTolerance}кПа[/color]{$nl
    }{$indent}[bullet/] Температура: [color=lightsalmon]{$temp}°К ± {$tempTolerance}°К[/color]{$nl
    }{$indent}[bullet/] Освещение: [color=gray][bold]{$lightLevel} ± {$lightTolerance}[/bold][/color]{$nl
    }[bullet/] Цветы: {$yield ->
        [-1]{LOC("plant-analyzer-printout-missing")}
        [0][color=red]0[/color]
        *[other][color=lightgreen]{$yield} {$potency}[/color]
    }{$nl
    }[bullet/] Семена: {$seeds ->
        [no][color=red]Нет[/color]
        [yes][color=green]Да[/color]
        *[other]{LOC("plant-analyzer-printout-missing")}
    }{$nl
    }[bullet/] Химикаты: [color=gray][bold]{$chemicals}[/bold][/color]{$nl
    }[bullet/] Выбросы: [bold]{$gasesOut}[/bold]
