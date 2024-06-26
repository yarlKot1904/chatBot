import { useEffect, useState } from 'react';
import '../styles.css';

const URL = '/api/posts';

// Компонент Chat
const Chat = () => {
    const [messages, setMessages] = useState([]);
    const [lastCommands, setLastCommands] = useState([]);

    // Функция для отправки сообщения от пользователя
    const sendMessage = async (textFromUser) => {
        if (textFromUser === undefined) {
            textFromUser = document.querySelector('#message').value;
        }

        // Создание сообщения пользователя
        const userMessage = {
            header: 'user',
            timestamp: new Date().toISOString(),
            text: textFromUser,
        };

        // Опции запроса
        const headers = new Headers();
        headers.set('Content-Type', 'application/json');
        const options = {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(userMessage),
        };

        try {
            // Отправка запроса на сервер
            const result = await fetch(URL, options);

            if (result.ok) {
                const serverResponse = await result.json();

                // Разделение ответа на пользователя и бота по символу '&&'
                const [userText, botText] = serverResponse.text.split('&&');

                // Создание объектов сообщений пользователя и бота
                const updatedUserMessage = { ...userMessage, text: userText };
                const botMessage = {
                    header: 'bot',
                    timestamp: serverResponse.timestamp,
                    text: botText,
                };

                // Добавляем сообщение от пользователя и ответ от бота в массив сообщений
                setMessages((prevMessages) => [...prevMessages, updatedUserMessage, botMessage]);

                // Очистка поля ввода после успешной отправки сообщения
                document.querySelector('#message').value = '';

                // Автоматическая прокрутка вниз после добавления новых сообщений
                scrollToBottom();
            } else {
                console.error('Ошибка при отправке запроса на сервер');
            }
        } catch (error) {
            console.error('Ошибка при отправке запроса на сервер', error);
        }
    };

    // Функция для автоматической прокрутки вниз
    const scrollToBottom = () => {
        const chatContainer = document.querySelector('#chatContainer');
        chatContainer.scrollTop = chatContainer.scrollHeight;
    };

    // Загрузка всех сообщений при первоначальной загрузке компонента
    useEffect(() => {
        const getMessages = async () => {
            const options = {
                method: 'GET',
            };
            const result = await fetch(URL, options);

            if (result.ok) {
                const fetchedMessages = await result.json();
                setMessages(fetchedMessages);

                // Автоматическая прокрутка вниз после загрузки всех сообщений
                scrollToBottom();
            }
        };

        getMessages();
    }, []);

    // Обновление массива последних команд
    useEffect(() => {
        const updateLastCommands = () => {
            const commandsSet = new Set();

            // Проходим в обратном порядке по массиву сообщений
            for (let i = messages.length - 1; i >= 0 && commandsSet.size < 3; i--) {
                const message = messages[i];
                const [text, buttonSection] = message.text.split('#');

                if (buttonSection) {
                    const buttons = buttonSection.split('^');

                    for (const button of buttons) {
                        const [buttonName] = button.split('|');
                        if (buttonName.startsWith('/')) {
                            commandsSet.add(buttonName);
                        }

                        // Останавливаемся, если набрали 3 команды
                        if (commandsSet.size >= 3) {
                            break;
                        }
                    }
                }
            }

            // Устанавливаем массив последних команд из Set
            setLastCommands(Array.from(commandsSet));
            scrollToBottom();
        };

        updateLastCommands();
    }, [messages]);

    return (
        <div id="chatContainer">
            

            {/* Сообщения */}
            <div>
                {messages.map((message, index) => (
                    <MessageItem key={index} message={message} sendMessage={sendMessage} />
                ))}
            </div>

            {/* Кнопки последних команд над полем ввода */}
            <div id="lastCommands">
                {lastCommands.map((command, index) => (
                    <button id="commandButton" className="command-button" key={index} onClick={() => sendMessage(command)}>
                        {command}
                    </button>
                ))}
            </div>

            {/* Поле ввода и кнопка отправки */}
            <div id="inputArea">
                <textarea id="message" placeholder="Напишите свой вопрос"></textarea>
                <button id="sendButton" className="send-button" onClick={() => sendMessage()}>
                    <img src="/assets/send-icon.svg" alt="Отправить" className="send-button-icon" />
                </button>
            </div>
        </div>
    );
};

// Компонент MessageItem
const MessageItem = ({ message, sendMessage }) => {
    // Определение, от кого сообщение: от пользователя или от бота
    const isUserMessage = message.header === 'user';

    // Разделение текста ответа бота и кнопок по символу '#'
    const [text, buttonSection] = message.text.split('#');

    return (
        <div className={`messageContainer ${isUserMessage ? 'userMessage' : 'botMessage'}`}>
            <div>
                <p>{text}</p>
                <small>{message.timestamp}</small>

                {/* Отображение кнопок */}
                {buttonSection && (
                    <div className="buttonContainer">
                        {buttonSection.split('^').map((button, index) => {
                            const [buttonName, buttonUrl] = button.split('|');
                            if (buttonName.startsWith('/')) {
                                return (
                                    <button key={index} onClick={() => sendMessage(buttonName)}>
                                        {buttonName}
                                    </button>
                                );
                            } else {
                                return (
                                    <button key={index} onClick={() => window.open(buttonUrl, '_blank')}>
                                        {buttonName}
                                    </button>
                                );
                            }
                        })}
                    </div>
                )}
            </div>
        </div>
    );
};

export default Chat;
