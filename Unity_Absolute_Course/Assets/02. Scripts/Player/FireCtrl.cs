using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;

[Serializable]
public struct PlayerSfx
{
    public AudioClip[] fire;
    public AudioClip[] reload;
}

public class FireCtrl : MonoBehaviour
{
    //무기 타입
    public enum WeaponType
    {
        RIFLE=0,
        SHOTGUN
    }

    //주인공이 현재 들고있는 무기를 저장할 변수
    public WeaponType currWeapon = WeaponType.RIFLE;

    //총알 프리팹
    public GameObject bullet;

    //탄피 프리팹
    public ParticleSystem cartridge;

    //총구 화염 프리팹
    public ParticleSystem muzzleFlash;

    //오디오소스 컴포넌트를 저장할 변수
    private AudioSource _audio;
  
    //총알 발사 좌표
    public Transform firePos;

    //오디오 클립을 저장할 변수
    public PlayerSfx playerSfx;

    // Shake 클래스를 저장할 변수
    private Shake shake;

    public Image magazineImg;
    public Text magazineText;

    public int maxBullet = 10;
    public int remainingBullet=10;

    public float reloadTime = 2.0f;
    private bool isReloading = false;

    // Start is called before the first frame update
    void Start()
    {
        //FirePos 하위에 있는 컴포넌트 추출
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
        //AudioSource 컴포넌트 추출
        _audio = GetComponent<AudioSource>();

        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReloading&&Input.GetMouseButtonDown(0))
        {
            --remainingBullet;
            Fire();

            if (remainingBullet == 0)
            {
                StartCoroutine(Reloading());
            }
        }
    }

    void Fire()
    {
        //Shake 효과 호출
        StartCoroutine(shake.ShakeCamera());
        //bullet 프리팹을 동적으로 생성
        //Instantiate(bullet, firePos.position, firePos.rotation);
        var _bullet = GameManager.instance.GetBullet();
        if(_bullet != null)
        {
            _bullet.transform.position = firePos.position;
            _bullet.transform.rotation = firePos.rotation;
            _bullet.SetActive(true);
        }
        //파티클 실행
        cartridge.Play();
        muzzleFlash.Play();
        //사운드 발생
        FireSfx();

        magazineImg.fillAmount = (float)remainingBullet / (float)maxBullet;
        UpdateBulletText();
    }

    void FireSfx()
    {
        //현재 들고 있는 무기의 오디오 클립을 가져옴
        var _sfx = playerSfx.fire[(int)currWeapon];
        //사운드 발생
        _audio.PlayOneShot(_sfx, 1.0f);
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        _audio.PlayOneShot(playerSfx.reload[(int)currWeapon], 1.0f);

        yield return new WaitForSeconds(playerSfx.reload[(int)currWeapon].length + 0.3f);

        isReloading = false;
        magazineImg.fillAmount = 1.0f;
        remainingBullet = maxBullet;
        UpdateBulletText();

    }

    void UpdateBulletText()
    {
        magazineText.text = string.Format("<color=#ff0000>{0}</color>/{1}", remainingBullet, maxBullet);
    }
}
