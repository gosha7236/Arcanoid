using Arcanoid.Clasess;

namespace Arcanoid
{

    public partial class MainForm : Form
    {
        /// <summary>
        /// метод для основной формы, где мы инициализируем компоненты 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            // Настройки окна
            this.Text = "Арканоид - WinForms";
            this.ClientSize = new Size(800, 600);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.DoubleBuffered = true; // убираем мерцание при отрисовке

            InitializeGame(); // создаём начальные объекты

            // === Подписка на события ===
            this.Paint += GameForm_Paint;          // отрисовка
            this.KeyDown += GameForm_KeyDown;      // нажатие клавиш
            this.KeyUp += GameForm_KeyUp;          // отпускание клавиш
            this.MouseMove += GameForm_MouseMove;  // движение мыши
            this.MouseClick += GameForm_MouseClick;// клик мышью

            // === Игровой таймер (примерно 60 кадров/с) ===
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 16;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }
        // === Основные игровые объекты ===
        private Rectangle paddle;        // Платформа (ракетка)
        private Rectangle ball;          // Шарик
        private List<Brick> bricks = new List<Brick>(); // Список кирпичей

        // === Параметры состояния игры ===
        private int paddleWidth = 100;
        private int paddleHeight = 12;
        private int ballSize = 12;

        private float ballVelX = 4f;     // Скорость шара по X
        private float ballVelY = -4f;    // Скорость шара по Y

        private bool moveLeft = false;   // Флаг — идёт ли движение влево
        private bool moveRight = false;  // Флаг — идёт ли движение вправо
        private bool ballStuckToPaddle = true; // Шар прикреплён к платформе (в начале игры)

        private System.Windows.Forms.Timer gameTimer;         // Таймер обновления (игровой цикл)
        private int lives = 3;           // Количество жизней
        private int score = 0;           // Очки
        private bool isPaused = false;   // Пауза

        // === Вспомогательные графические параметры ===
        private Font hudFont = new Font("Arial", 12);
        private Brush brickBrush1 = Brushes.SteelBlue;
        private Brush brickBrush2 = Brushes.OrangeRed;
        private Brush paddleBrush = Brushes.DarkGreen;
        private Brush ballBrush = Brushes.Black;

        /// <summary>
        /// Инициализация игры (начало нового раунда)
        /// </summary>
        private void InitializeGame()
        {
            // Центрируем платформу снизу
            paddle = new Rectangle((ClientSize.Width - paddleWidth) / 2,
                                   ClientSize.Height - 40,
                                   paddleWidth,
                                   paddleHeight);

            // Размещаем шар прямо над платформой
            ball = new Rectangle(paddle.X + (paddleWidth - ballSize) / 2,
                                 paddle.Y - ballSize - 2,
                                 ballSize,
                                 ballSize);

            // Сбрасываем скорости шара
            ballVelX = 4f;
            ballVelY = -4f;
            ballStuckToPaddle = true;

            // Создаём кирпичи
            CreateBricks();

            // Обнуляем счёт и жизни
            lives = 3;
            score = 0;
            isPaused = false;
        }

        /// <summary>
        /// Создание сетки кирпичей
        /// </summary>
        private void CreateBricks()
        {
            bricks.Clear();

            int rows = 6;           // количество рядов кирпичей
            int cols = 10;          // количество столбцов
            int padding = 4;        // отступ между кирпичами
            int topOffset = 50;     // отступ сверху
            int brickWidth = (ClientSize.Width - (cols + 1) * padding) / cols;
            int brickHeight = 22;

            // Цвета для каждого ряда (от нижнего к верхнему)
            Color[] rowColors =
            {
            Color.SkyBlue,     // нижний ряд (1 удар)
            Color.LightGreen,  // 2 удара
            Color.Gold,        // 3 удара
            Color.Orange,      // 4 удара
            Color.IndianRed,   // 5 ударов
            Color.DarkRed      // верхний ряд (6 ударов)
                };

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int x = padding + c * (brickWidth + padding);
                    int y = topOffset + r * (brickHeight + padding);

                    // Нижний ряд = 1 удар, верхний = rows ударов
                    int hits = (rows - r);

                    // Добавляем кирпич с цветом по ряду
                    bricks.Add(new Brick(new Rectangle(x, y, brickWidth, brickHeight), hits, rowColors[r]));
                }
            }
        }

        /// <summary>
        /// Главный игровой цикл — вызывается каждый кадр (~60 раз в секунду)
        /// </summary>
        private void GameLoop(object sender, EventArgs e)
        {
            if (isPaused)
            {
                Invalidate(); return;
            }

            // === Управление платформой ===
            int paddleSpeed = 8;
            if (moveLeft)
            {
                paddle.X = Math.Max(0, paddle.X - paddleSpeed);
            }
            if (moveRight)
            {
                paddle.X = Math.Min(ClientSize.Width - paddle.Width, paddle.X + paddleSpeed);
            }

            // === Если шар "прилип" к платформе ===
            if (ballStuckToPaddle)
            {
                ball.X = paddle.X + (paddle.Width - ball.Width) / 2;
                ball.Y = paddle.Y - ball.Height - 2;
            }
            else
            {
                // === Движение шара ===
                ball.X = (int)Math.Round(ball.X + ballVelX);
                ball.Y = (int)Math.Round(ball.Y + ballVelY);

                // === Проверка столкновений со стенами ===
                if (ball.X <= 0)
                {
                    ball.X = 0;
                    ballVelX = -ballVelX;
                }
                if (ball.Right >= ClientSize.Width)
                {
                    ball.X = ClientSize.Width - ball.Width;
                    ballVelX = -ballVelX;
                }
                if (ball.Y <= 0)
                {
                    ball.Y = 0;
                    ballVelY = -ballVelY;
                }

                // === Проверка столкновения с платформой ===
                if (ball.IntersectsWith(paddle) && ballVelY > 0)
                {
                    // Угол отражения зависит от точки касания
                    float hitPos = (ball.X + ball.Width / 2f) - (paddle.X + paddle.Width / 2f);
                    float normalized = hitPos / (paddle.Width / 2f); // значение от -1 до 1
                    float maxAngle = (float)(Math.PI / 3); // максимум 60 градусов
                    float angle = normalized * maxAngle;
                    float speed = (float)Math.Sqrt(ballVelX * ballVelX + ballVelY * ballVelY);
                    ballVelX = speed * (float)Math.Sin(angle);
                    ballVelY = -Math.Abs(speed * (float)Math.Cos(angle));
                    ball.Y = paddle.Y - ball.Height - 1; // чтобы не "застревал"
                }

                // === Проверка столкновения с кирпичами ===
                for (int i = 0; i < bricks.Count; i++)
                {
                    var br = bricks[i];
                    if (!br.Visible)  continue;

                    if (ball.IntersectsWith(br.Rect))
                    {
                        // Определяем, с какой стороны произошло столкновение
                        Rectangle prevBall = new Rectangle(
                            (int)Math.Round(ball.X - ballVelX),
                            (int)Math.Round(ball.Y - ballVelY),
                            ball.Width,
                            ball.Height);

                        bool collidedHorizontally =
                            prevBall.Bottom > br.Rect.Top && prevBall.Top < br.Rect.Bottom &&
                            (prevBall.Right <= br.Rect.Left || prevBall.Left >= br.Rect.Right);

                        bool collidedVertically = prevBall.Right > br.Rect.Left && prevBall.Left < br.Rect.Right &&
                            (prevBall.Bottom <= br.Rect.Top || prevBall.Top >= br.Rect.Bottom);

                        // Меняем направление в зависимости от стороны
                        if (collidedHorizontally)
                            ballVelX = -ballVelX;
                        else if (collidedVertically)
                            ballVelY = -ballVelY;
                        else
                            ballVelY = -ballVelY;

                        // Уменьшаем "прочность" кирпича
                        br.Hits--;
                        if (br.Hits <= 0)
                        {
                            bricks.Remove(br);
                            score += 100;
                        }
                        else
                        {
                            score += 50;
                        }
                        break; // один кирпич за кадр
                    }
                }

                // === Проверяем, не упал ли шар ===
                if (ball.Y > ClientSize.Height)
                {
                    lives--;
                    if (lives == 0)
                    {
                        // Конец игры
                        var res = MessageBox.Show(
                            $"Игра окончена! Счёт: {score}\nНачать заново?",
                            "Game Over",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (res == DialogResult.Yes)
                            InitializeGame();
                        else
                            Application.Exit();
                    }
                    else
                    {
                        // Потеряли жизнь — возвращаем шар на платформу
                        ballStuckToPaddle = true;
                        ballVelX = 4f;
                        ballVelY = -4f;
                        ball.X = paddle.X + (paddle.Width - ball.Width) / 2;
                        ball.Y = paddle.Y - ball.Height - 2;
                    }
                }

                // === Проверяем победу (все кирпичи уничтожены) ===
                if (bricks.All(b => !b.Visible))
                {
                    gameTimer.Stop();
                    var dialogResult = MessageBox.Show(
                        $"Ты очистил уровень! Счёт: {score}\nПродолжить (новый уровень)?",
                        "Победа",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (dialogResult == DialogResult.Yes)
                    {
                        // Создаём новые кирпичи и чуть увеличиваем скорость
                        CreateBricks();
                        ballVelX *= 1.1f;
                        ballVelY *= 1.1f;
                        ballStuckToPaddle = true;
                        ball.X = paddle.X + (paddle.Width - ball.Width) / 2;
                        ball.Y = paddle.Y - ball.Height - 2;
                        gameTimer.Start();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }

            Invalidate(); // Перерисовать кадр
        }
        /// <summary>
        /// Отрисовка всех объектов
        /// </summary>
        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // Фон
            g.Clear(Color.FromArgb(30, 30, 30));
            // Кирпичи
            foreach (var br in bricks)
            {
                if (!br.Visible) continue;
                using (SolidBrush brush = new SolidBrush(br.Color))
                {
                    g.FillRectangle(brush, br.Rect);

                }
            }
            // Платформа
            g.FillRectangle(paddleBrush, paddle);
            g.DrawRectangle(Pens.Black, paddle);
            // Шар
            g.FillEllipse(ballBrush, ball);
            g.DrawEllipse(Pens.White, ball);
            // Отображение счёта и жизней
            string hud = $"Счёт: {score}   Жизни: {lives}   (Пауза: {(isPaused ? "Вкл" : "Выкл")})";
            g.DrawString(hud, hudFont, Brushes.White, new PointF(8, ClientSize.Height - 28));
        }
        /// <summary>
        /// Обработка нажатия клавиш
        /// </summary>
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) moveLeft = true;
            if (e.KeyCode == Keys.Right) moveRight = true;

            if (e.KeyCode == Keys.P) // пауза
                isPaused = !isPaused;

            if (e.KeyCode == Keys.Space) // отпустить шар
            {
                if (ballStuckToPaddle)
                {
                    ballStuckToPaddle = false;
                    var rnd = new Random();
                    ballVelX = (float)(3 + rnd.NextDouble() * 2) * (rnd.Next(0, 2) == 0 ? -1 : 1);
                    ballVelY = -4f;
                }
            }
            if (e.KeyCode == Keys.R) // рестарт
                InitializeGame();

            if (e.KeyCode == Keys.Escape) // выход
                Application.Exit();
        }
        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) moveLeft = false;
            if (e.KeyCode == Keys.Right) moveRight = false;
        }
        /// <summary>
        /// Движение платформы мышью
        /// </summary>
        private void GameForm_MouseMove(object sender, MouseEventArgs e)
        {
            int mx = e.X - paddle.Width / 2;
            paddle.X = Math.Max(0, Math.Min(ClientSize.Width - paddle.Width, mx));

            // если шар "приклеен" — двигаем его вместе с платформой
            if (ballStuckToPaddle)
            {
                ball.X = paddle.X + (paddle.Width - ball.Width) / 2;
                ball.Y = paddle.Y - ball.Height - 2;
            }
        }
        /// <summary>
        /// Клик мышью — отпустить шар
        /// </summary>
        private void GameForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ballStuckToPaddle)
            {
                ballStuckToPaddle = false;
                var rnd = new Random();
                ballVelX = (float)(3 + rnd.NextDouble() * 2) * (rnd.Next(0, 2) == 0 ? -1 : 1);
                ballVelY = -4f;
            }
        }
    }
}
