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

        if (newPosition.x > ConstantsHandler.ScreenRangeX || newPosition.x < -ConstantsHandler.ScreenRangeX) {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }

        if (newPosition.y > -ConstantsHandler.ScreenRangeY || newPosition.y < ConstantsHandler.ScreenRangeY) {
            newPosition.y = -newPosition.y;
            isWrappingX = true;
        }

        transform.position = newPosition;
    }

    private bool CheckRenderers() {
        var position = transform.position;
        if (!(position.x < ConstantsHandler.ScreenRangeX) || !(position.x > -ConstantsHandler.ScreenRangeX)) {
            return false;
        }

        return position.y > -ConstantsHandler.ScreenRangeY && position.y < ConstantsHandler.ScreenRangeY;
    }
}