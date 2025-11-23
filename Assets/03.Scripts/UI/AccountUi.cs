using Firebase;
using Firebase.Auth;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountUi : MonoBehaviour
{
    public GameObject AccountPannel;
    public TMP_InputField IdInput;
    public TMP_InputField PwInput;

    [Header("Message")]
    public GameObject CheckMessagePanel;
    public TextMeshProUGUI MessageTxt;
    private readonly float _pannelViewTime = 2.0f;
    private Coroutine _messageCoroutine;

    [Header("Button")]
    public Button LoginBtn;
    public Button RegisterNewUserBtn;
    public Button GuestBtn;

    private FirebaseAuth _auth;

    private void Awake()
    {
        // === 파이어 베이스 인증 시스템을 초기화 ===
        _auth = FirebaseAuth.DefaultInstance;

        CheckMessagePanel.SetActive(false);

        LoginBtn.onClick.AddListener(SignInUser);
        RegisterNewUserBtn.onClick.AddListener(RegisterNewUser);
        GuestBtn.onClick.AddListener(SignInGuest);
    }

    // === 로그인 ===
    private async void SignInUser()
    {
        string email = IdInput.text;
        string password = PwInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            MessageTxt.text = "이메일과 비밀번호를 입력해주세요.";
            ShowMessage();
            return;
        }

        try
        {
            Debug.Log($"로그인 시도 중: {email}");

            Task<AuthResult> signInTask = _auth.SignInWithEmailAndPasswordAsync(email, password);
            await signInTask;

            if (signInTask.IsCompletedSuccessfully)
            {
                FirebaseUser user = signInTask.Result.User;
                MessageTxt.text = $"로그인 성공! 사용자: {user.Email},\\ UID: {user.UserId}";
                ShowMessage();
                AccountPannel.SetActive(false);
            }
        }
        catch (FirebaseException e)
        {
            LoginError((AuthError)e.ErrorCode);
        }
    }

    // === 회원가입 ===
    private async void RegisterNewUser()
    {
        string email = IdInput.text;
        string password = PwInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            MessageTxt.text = "이메일과 비밀번호를 입력해주세요.";
            ShowMessage();
            return;
        }

        try
        {
            MessageTxt.text = "회원가입 중...";
            ShowMessage();

            Task<AuthResult> registerTask = _auth.CreateUserWithEmailAndPasswordAsync(email, password);
            await registerTask; 

            if (registerTask.IsCompletedSuccessfully)
            {
                FirebaseUser newUser = registerTask.Result.User;
                MessageTxt.text = $"회원가입 성공! UID: {newUser.UserId},\\ Email: {newUser.Email}";
                ShowMessage();
            }
        }
        catch (FirebaseException e)
        {
            LoginError((AuthError)e.ErrorCode);
        }
    }

    // === (익명)게스트 로그인 ===
    private async void SignInGuest()
    {
        // === 이전 사용자 로그아웃 ===
        if (_auth.CurrentUser != null)
        {
            _auth.SignOut();
        }

        try
        {
            MessageTxt.text = "게스트 로그인 시도 중...";
            ShowMessage();

            Task<AuthResult> guestTask = _auth.SignInAnonymouslyAsync();
            await guestTask;


            if (guestTask.IsCompletedSuccessfully)
            {
                FirebaseUser user = guestTask.Result.User;
                MessageTxt.text = $"게스트 로그인 성공!\\ 익명 UID: {user.UserId}";
                ShowMessage();
                AccountPannel.SetActive(false);
            }
        }
        catch (FirebaseException e)
        {
            MessageTxt.text = $"게스트 로그인 실패: {e.ErrorCode} - {e.Message}";
            ShowMessage();
        }
    }

    // === 에러 처리 ===
    private void LoginError(AuthError errorCode)
    {
        switch (errorCode)
        {
            case AuthError.WeakPassword:
                MessageTxt.text = "비밀번호가 너무 짧습니다 (6자 이상이어야 합니다).";
                break;
            case AuthError.InvalidEmail:
                MessageTxt.text = "유효하지 않은 이메일 형식입니다.";
                break;
            case AuthError.EmailAlreadyInUse:
                MessageTxt.text = "이미 등록된 이메일 주소입니다.";
                break;
            case AuthError.UserNotFound: 
                MessageTxt.text = "등록된 사용자가 없거나 이메일이 잘못되었습니다.";
                break;
            case AuthError.WrongPassword: 
                MessageTxt.text = "비밀번호가 일치하지 않습니다.";
                break;
            case AuthError.TooManyRequests: 
                MessageTxt.text = "너무 많은 시도가 있었습니다. 잠시 후 다시 시도해 주세요.";
                break;
        }

        ShowMessage();
    }

    // === 메세지 출력 ===
    private void ShowMessage()
    {
        if(_messageCoroutine != null)
        {
            StopCoroutine(HideMessagePannel());
        }

        CheckMessagePanel.SetActive(true);

        _messageCoroutine = StartCoroutine(HideMessagePannel());
    }
    
    private IEnumerator HideMessagePannel()
    {
        yield return new WaitForSeconds(_pannelViewTime);

        CheckMessagePanel.SetActive(false);
    }
}
