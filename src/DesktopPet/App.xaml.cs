using System.Threading;
using System.Windows;
using MessageBox = System.Windows.MessageBox;
using MessageBoxButton = System.Windows.MessageBoxButton;
using MessageBoxImage = System.Windows.MessageBoxImage;
using Path = System.IO.Path;
using DesktopPet.Engine;
using DesktopPet.Services;
using DesktopPet.UI;

namespace DesktopPet;

public partial class App : System.Windows.Application
{
    private Mutex? _singleInstance;
    private PrankWindow? _setup;
    private MouseMonitor? _triggerMouse;
    private SystemMonitor? _system;
    private PetWindow? _petWindow;
    private PetEngine? _engine;
    private SpriteAnimator? _animator;
    private SurfaceProvider? _surface;
    private StateMachine? _stateMachine;
    private AppSettings? _settings;
    private string _prankMessage = "Tu nivel de estupidez es muy alto.";

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _singleInstance = new Mutex(true, "GatoBroma.SingleInstance", out bool isNew);
        if (!isNew) { Shutdown(); return; }

        ShowSetup();
    }

    private void ShowSetup()
    {
        _setup = new PrankWindow();
        _setup.Programmed += OnProgrammed;
        _setup.Closed += (_, _) =>
        {
            if (!_setup!.IsProgrammed)
                Shutdown();
        };
        _setup.Show();
        _setup.Activate();
    }

    private void OnProgrammed(PrankTrigger trigger, string message)
    {
        _prankMessage = message;
        // The setup window closes before the global mouse hook is installed,
        // so the "Programar" click can never count as the first click.
        _setup?.Close();
        _setup = null;

        if (trigger == PrankTrigger.FirstClick)
        {
            _triggerMouse = new MouseMonitor();
            _triggerMouse.LeftButtonDown += OnFirstClick;
        }
        else
        {
            int seconds = (int)trigger;
            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(seconds)
            };
            timer.Tick += (_, _) =>
            {
                timer.Stop();
                LaunchPrank();
            };
            timer.Start();
        }
    }

    private void OnFirstClick()
    {
        if (_triggerMouse == null) return;
        _triggerMouse.LeftButtonDown -= OnFirstClick;
        _triggerMouse.Dispose();
        _triggerMouse = null;
        Dispatcher.BeginInvoke(new Action(LaunchPrank));
    }

    private void LaunchPrank()
    {
        _triggerMouse?.Dispose();
        _triggerMouse = null;

        MessageBox.Show(
            _prankMessage,
            "Error Windows",
            MessageBoxButton.OK,
            MessageBoxImage.Error);

        StartCat();
    }

    private void StartCat()
    {
        _settings = new AppSettings
        {
            ActivePet = "cat",
            Speed = 1.0,
            SizeScale = 4.0,
            EnableWindowWalking = true,
            EnableCursorChase = false,
            EnableSleep = false,
            EnableMoods = false,
            EnableSystemReactions = false,
            EnableAutoUpdate = false,
            StretchIntervalMinutes = 0,
            EnableAiCompanion = false
        };

        string petDir = Path.Combine(AppContext.BaseDirectory, "Assets", "pets", "cat");
        var manifest = PetManifest.Load(Path.Combine(petDir, "manifest.json"));
        _animator = new SpriteAnimator(manifest, petDir);

        _petWindow = new PetWindow();
        _petWindow.Show();

        _triggerMouse = new MouseMonitor();
        _system = null;

        _surface = new SurfaceProvider(() => _petWindow.Handle);
        _stateMachine = new StateMachine(_settings);
        _engine = new PetEngine(_petWindow, _animator, _surface, _stateMachine, _settings,
                                keyboard: null, mouse: _triggerMouse, system: null);
        _petWindow.Attach(_engine, _engine.Width, _engine.Height);

        _engine.StartPrank();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        try { _triggerMouse?.Dispose(); } catch { }
        try { _singleInstance?.Dispose(); } catch { }
        base.OnExit(e);
    }
}
