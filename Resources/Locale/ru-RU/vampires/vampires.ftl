vampires-title = Вампиры
metabolizer-type-vampire = Vampire

vampire-fangs-extended-examine = Вы замечаете блеск [color=white]острых клыков[/color]
vampire-fangs-extended = Вы обнажаете клыки
vampire-fangs-retracted = Вы прячете клыки

vampire-blooddrink-empty = В этом теле совсем нет крови
vampire-blooddrink-rotted = Тело гниёт, а кровь испорчена
vampire-blooddrink-zombie = Их кровь осквернена смертью

vampire-startlight-burning = Вы чувствуете, как кожа горит в свете тысячи солнц

vampire-not-enough-blood = У вас недостаточно крови
vampire-cuffed = Ваши руки должны быть свободны!
vampire-stunned = Вы не можете сосредоточиться!
vampire-muffled = На ваш рот надет намордник
vampire-full-stomach = Вы раздулись от выпитой крови

vampire-deathsembrace-bind = Совсем как дома...

vampire-ingest-holyblood = Ваш рот горит!

vampire-cloak-enable = Вы окутываете себя тенями
vampire-cloak-disable = Вы отпускаете тени

vampire-bloodsteal-other = Вы чувствуете, как кровь буквально вырывается из вашего тела!
vampire-bloodsteal-no-victims = Вы пытаетесь вытянуть кровь, но поблизости нет жертв — ваша сила растворяется в воздухе!
vampire-hypnotise-other = {CAPITALIZE(THE($user))} пристально смотрит в {MAKEPLURAL(THE($target))} глаза!
vampire-unnaturalstrength = Мышцы {CAPITALIZE(THE($user))} напрягаются, делая его сильнее!
vampire-supernaturalstrength = Тело {CAPITALIZE(THE($user))} наполняется потусторонней мощью, делая его невероятно сильным!

store-currency-display-blood-essence = Эссенция крови
store-category-vampirepowers = Способности
store-category-vampirepassives = Пассивные навыки

# Powers

# Passives
vampire-passive-unholystrength = Нечестивая сила
vampire-passive-unholystrength-description = Наполните мышцы эссенцией, получая когти и увеличенную силу. Эффект: 10 единиц режущего урона за удар.

vampire-passive-supernaturalstrength = Сверхъестественная мощь
vampire-passive-supernaturalstrength-description = Ещё больше укрепляет мышцы — ни одна преграда не устоит перед вами. Эффект: 15 единиц режущего урона за удар, возможность взламывать двери голыми руками.

vampire-passive-deathsembrace = Объятия смерти
vampire-passive-deathsembrace-description = Примите смерть, и она обойдёт вас стороной. Эффект: исцеление в гробу; автоматическое возвращение в гроб после смерти за 100 единиц эссенции крови.

# Mutation menu

vampire-mutation-menu-ui-window-name = Меню мутаций

vampire-mutation-none-info = Ничего не выбрано

vampire-mutation-hemomancer-info =
    Гемомант

    Специализируется на магии крови и манипулировании кровью вокруг себя.

    Способности:

    - Визг
    - Похищение крови

vampire-mutation-umbrae-info =
    Тень

    Специализируется на тьме, скрытности и мобильности.

    Способности:

    - Ослепительный взгляд
    - Покров тьмы

vampire-mutation-gargantua-info =
    Гаргантюа

    Специализируется на ближнем бое и стойкости.

    Способности:

    - Нечестивая сила
    - Сверхъестественная мощь

vampire-mutation-bestia-info =
    Бестия

    Фокусируется на превращении и собирании трофеев

    Abilities:

    - Форма летучей мыши
    - Форма мыши

## Objectives

objective-condition-drain-title = Высосать { $count } ед. крови.
objective-condition-drain-description = Я должен выпить { $count } ед. крови. Это необходимо для моего выживания и дальнейшей эволюции.
ent-VampireSurviveObjective = Выжить
    .desc = Я должен выжить любой ценой.
ent-VampireEscapeObjective = Покинуть станцию живым и свободным.
    .desc = Я должен улететь на эвакуационном шаттле. Будучи на свободе.

## Alert

alerts-vampire-blood-name = Запас эссенции крови
alerts-vampire-blood-desc = Текущее количество вашей вампирской эссенции.
alerts-vampire-stellar-weakness-name = Звёздная уязвимость
alerts-vampire-stellar-weakness-desc = Вы горите под светом солнца или, если быть точнее, под излучением нескольких миллиардов звёзд, воздействию которых вы подвергаетесь за пределами станции.


## Preset

vampire-roundend-name = Вампир
objective-issuer-vampire = [color=red]Жажда крови[/color]
roundend-prepend-vampire-drained-named = [color=white]{ $name }[/color] выпил в общей сложности [color=red]{ $number }[/color] ед. крови.
roundend-prepend-vampire-drained = Кто-то выпил в общей сложности [color=red]{ $number }[/color] ед. крови.
vampire-gamemode-title = Вампиры
vampire-gamemode-description = Кровожадные вампиры проникли на станцию, чтобы испить крови!
vampire-role-greeting =
    Вы — вампир, пробравшийся на станцию под видом обычного сотрудника!
    Ваши цели указаны в меню персонажа.
    Пейте кровь и эволюционируйте, чтобы достичь их!
vampire-role-greeting-short = Вы — вампир, пробравшийся на станцию под видом обычного сотрудника!
roles-antag-vamire-name = Вампир

## Actions

ent-ActionVampireOpenMutationsMenu = Меню мутаций
    .desc = Открывает меню с мутациями вампира.
ent-ActionVampireToggleFangs = Выпустить клыки
    .desc = Выпустить или спрятать клыки. Прогулка с обнажёнными клыками может выдать вашу истинную сущность.
ent-ActionVampireGlare = Ослепляющий взгляд
    .desc = Ваши глаза испускают яркую вспышку, оглушая незащищённого смертного на 10 секунд. Стоимость: 20 эссенции. Перезарядка: 60 секунд.
ent-ActionVampireHypnotise = Гипноз
    .desc = Пристальный взгляд в глаза смертного, погружающий его в сон на 60 секунд. Стоимость: 20 эссенции. Подготовка: 5 секунд. Перезарядка: 5 минут.
ent-ActionVampireScreech = Визг
    .desc = Пронзительный крик, оглушающий незащищённых смертных и разбивающий хрупкие предметы поблизости. Стоимость: 20 эссенции. Подготовка: 5 секунд. Перезарядка: 5 минут.
ent-ActionVampireBloodSteal = Похищение крови
    .desc = Принудительное извлечение крови из всех тел поблизости — живых или мёртвых. Стоимость: 20 эссенции. Перезарядка: 60 секунд.
ent-ActionVampireBatform = Облик летучей мыши
    .desc = Принятие формы летучей мыши. Быстрая, трудно попасть, любит фрукты. Стоимость: 20 эссенции. Перезарядка: 30 секунд.
ent-ActionVampireMouseform = Облик мыши
    .desc = Принятие формы мыши. Быстрая, маленькая, игнорирует двери. Стоимость: 20 эссенции. Перезарядка: 30 секунд.
ent-ActionVampireCloakOfDarkness = Покров тьмы
    .desc = Скрывает вас от глаз смертных, делая невидимым, пока вы неподвижны. Требуется крови: 330 эссенции. Стоимость активации: 30 эссенции. Поддержание: 1 эссенция/сек. Перезарядка: 10 секунд.
