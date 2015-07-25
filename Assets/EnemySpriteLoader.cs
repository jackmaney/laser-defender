using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpriteLoader : MonoBehaviour {

    public static List<Sprite> AllEnemySprites = new List<Sprite>();

    private static Dictionary<EnemyColor, Sprite[]> spritesByColor
        = new Dictionary<EnemyColor, Sprite[]>();

    private EnemyColor colors;
    private string[] colorNames;

	
    Sprite[] LoadByColor(EnemyColor enemyColor) {

        string path = "Sprites/Enemies/" + EnemyColorName(enemyColor);
        return Resources.LoadAll<Sprite>(path);
    }

    string EnemyColorName(EnemyColor color) {
        return colorNames[(int)color];

    }

    void Awake() {

        colorNames =
            new string[] { "Black", "Blue", "Green", "Red" };

        for(int i=0; i<4; i++) {
            EnemyColor color = (EnemyColor)i;
            Sprite[] sprites = LoadByColor(color);
            AllEnemySprites.AddRange(sprites);
            spritesByColor[color] = sprites;
        }
    }

    public Sprite Random() {
        int index =
            (int)UnityEngine.Random.Range(0, AllEnemySprites.Count);

        return AllEnemySprites[index];
    }

    public Sprite Random(EnemyColor color) {
        int index =
            (int)UnityEngine.Random.Range(
                0, spritesByColor[color].Length);

        return GetSprite(color, index);
    }

    public Sprite GetSprite(EnemyColor color, int index) {
        return spritesByColor[color][index];
    }
}
