using UnityEngine;

public class WrappingBox : MonoBehaviour {
    private bool isWrappingX;
    private bool isWrappingY;

    private void Update() {
        ScreenWrap();
    }

    private void ScreenWrap() {
        var isVisible = CheckRenderers();

        if (isVisible) {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }

        if (isWrappingX && isWrappingY) {
            return;
        }

        Vector2 newPosition = transform.position;

        if (newPosition.x > ConstantsHandler.SpawnRangeX || newPosition.x < -ConstantsHandler.SpawnRangeX) {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }

        if (newPosition.y > -ConstantsHandler.SpawnRangeY || newPosition.y < ConstantsHandler.SpawnRangeY) {
            newPosition.y = -newPosition.y;
            isWrappingX = true;
        }

        transform.position = newPosition;
    }

    private bool CheckRenderers() {
        var position = transform.position;
        if (!(position.x < ConstantsHandler.SpawnRangeX) || !(position.x > -ConstantsHandler.SpawnRangeX)) {
            return false;
        }

        return position.y > -ConstantsHandler.SpawnRangeY && position.y < ConstantsHandler.SpawnRangeY;
    }
}