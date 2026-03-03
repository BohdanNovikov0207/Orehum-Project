## UI

cargo-console-menu-title = Консоль заказа грузов
cargo-console-menu-account-name-label = Имя аккаунта:{ " " }
cargo-console-menu-account-name-none-text = Нет
cargo-console-menu-shuttle-name-label = Название шаттла:{ " " }
cargo-console-menu-shuttle-name-none-text = Нет
cargo-console-menu-points-label = Кредиты:{ " " }
cargo-console-menu-points-amount = ${ $amount }
cargo-console-menu-shuttle-status-label = Статус шаттла:{ " " }
cargo-console-menu-shuttle-status-away-text = Отбыл
cargo-console-menu-order-capacity-label = Объём заказов:{ " " }
cargo-console-menu-call-shuttle-button = Активировать телепад
cargo-console-menu-permissions-button = Доступы
cargo-console-menu-categories-label = Категории:{ " " }
cargo-console-menu-search-bar-placeholder = Поиск
cargo-console-menu-requests-label = Запросы
cargo-console-menu-orders-label = Заказы
cargo-console-menu-order-reason-description = Причина: { $reason }
cargo-console-menu-populate-categories-all-text = Все
cargo-console-menu-populate-orders-cargo-order-row-product-name-text = { $productName } (x{ $orderAmount }) от { $orderRequester }
cargo-console-menu-cargo-order-row-approve-button = Одобрить
cargo-console-menu-cargo-order-row-cancel-button = Отменить
cargo-console-menu-tab-title-orders = Заказы
cargo-console-menu-tab-title-funds = Переводы
cargo-console-menu-account-action-transfer-limit = [bold]Лимит перевода:[/bold] ${$limit}
cargo-console-menu-account-action-transfer-limit-unlimited-notifier = [color=gold](Безлимитно)[/color]
cargo-console-menu-account-action-select = [bold]Действие со счётом:[/bold]
cargo-console-menu-account-action-amount = [bold]Количество:[/bold] $
cargo-console-menu-account-action-button = Перевести
cargo-console-menu-toggle-account-lock-button = Переключить лимит перевода
cargo-console-menu-account-action-option-withdraw = Снять наличные
cargo-console-menu-account-action-option-transfer = Перевести доходы в {$code}
# Orders
cargo-console-order-not-allowed = Доступ запрещён
cargo-console-station-not-found = Нет доступной станции
cargo-console-invalid-product = Неверный ID продукта
cargo-console-too-many = Слишком много одобренных заказов
cargo-console-snip-snip = Заказ урезан до вместимости
cargo-console-insufficient-funds = Недостаточно средств (требуется { $cost })
cargo-console-unfulfilled = Нет места для выполнения заказа
cargo-console-trade-station = Отправить на { $destination }
cargo-console-unlock-approved-order-broadcast = [bold]Заказ на { $productName } x{ $orderAmount }[/bold], стоимостью [bold]{ $cost }[/bold], был одобрен [bold]{ $approver }[/bold]
cargo-console-fund-withdraw-broadcast = [bold]{$name} снял {$amount} кредитов с {$name1} \[{$code1}\]
cargo-console-fund-transfer-broadcast = [bold]{$name} перевёл {$amount} кредитов с {$name1} \[{$code1}\] в {$name2} \[{$code2}\][/bold]
cargo-console-fund-transfer-user-unknown = Неизвестно
# GoobStation - cooldown on Cargo Orders (specifically gamba)
cargo-console-cooldown-count = Невозможно заказать более одного {$product} за раз.
cargo-console-cooldown-active = Заказы на {$product} не могут быть размещены в течение еще {$timeCount} {$timeUnits}.
cargo-console-paper-print-name = Заказ #{ $orderNumber }
cargo-console-paper-print-text =
    Заказ #{ $orderNumber }
    Товар: { $itemName }
    Кол-во: { $orderQuantity }
    Запросил: { $requester }
    Причина: { $reason }
    Одобрил: { $approver }
# Cargo shuttle console
cargo-shuttle-console-menu-title = Консоль вызова грузового шаттла
cargo-shuttle-console-station-unknown = Неизвестно
cargo-shuttle-console-shuttle-not-found = Не найден
cargo-no-shuttle = Грузовой шаттл не найден!
cargo-shuttle-console-organics = На шаттле обнаружены органические формы жизни
# Funding allocation console
cargo-funding-alloc-console-menu-title = Консоль распределения доходов
cargo-funding-alloc-console-label-account = [bold]Счёт[/bold]
cargo-funding-alloc-console-label-code = [bold]Код[/bold]
cargo-funding-alloc-console-label-balance = [bold]Баланс[/bold]
cargo-funding-alloc-console-label-cut = [bold]Разделение доходов (%)[/bold]
cargo-funding-alloc-console-label-primary-cut = Доля дохода отдела снабжения от всего, кроме ящиков с замком (%):
cargo-funding-alloc-console-label-lockbox-cut = Доля дохода отдела снабжения от продажи ящиков с замком (%):
cargo-funding-alloc-console-label-help-non-adjustible = Отдел снабжения получает { $percent }% от всех доходов, кроме ящиков с замком. Остаток распределяется следующим образом:
cargo-funding-alloc-console-label-help-adjustible = Остаток доходов от всего, кроме ящиков с замком, распределяется следующим образом:
cargo-funding-alloc-console-button-save = Сохранить изменения
cargo-funding-alloc-console-label-save-fail = [bold]Разделения доходов недействительны![/bold] [color=red]({$pos ->
    [1] +
    *[-1] -
}{$val}%)[/color]
# Slip template
cargo-acquisition-slip-body = [head=3]Детали актива[/head]
    {"[bold]Товар:[/bold]"} {$product}
    {"[bold]Описание:[/bold]"} {$description}
    {"[bold]Стоимость за единицу:[/bold"}] ${$unit}
    {"[bold]Количество:[/bold]"} {$amount}
    {"[bold]Цена:[/bold]"} ${$cost}

    {"[head=3]Детали покупки[/head]"}
    {"[bold]Запросил:[/bold]"} {$orderer}
    {"[bold]Причина:[/bold]"} {$reason}
