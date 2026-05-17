/*
 This file is part of the MapleStory Unity

 Copyright (C) 2021-2026 Shen, Jen-Chieh <jcs090218@gmail.com>

 This program is free software: you can redistribute it and/or modify
 it under the terms of the GNU Affero General Public License version 3
 as published by the Free Software Foundation. You may not use, modify
 or distribute this program under any other version of the
 GNU Affero General Public License.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU Affero General Public License for more details.

 You should have received a copy of the GNU Affero General Public License
 along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using UnityEngine;
using JCSUnity;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;

namespace MSU
{
    /// MVP: load one static body sprite from Character.wz onto a SpriteRenderer.
    /// No equip / hair / face compositing yet.
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerLoader : MonoBehaviour
    {
        [Tooltip("Body skin image inside Character.wz (e.g. 00002000.img = default skin).")]
        public string bodyImg = "00002000.img";

        [Tooltip("Animation state to take the first frame from.")]
        public string animState = "stand1";

        private SpriteRenderer sr;

        private void Awake() { sr = GetComponent<SpriteRenderer>(); }

        private void Start()
        {
            var wzfm = WzFileManager.FirstInstance();
            WzDirectory ch;
            try { ch = wzfm.GetWzDir("Character"); }
            catch (System.Exception e) { Debug.LogError("[PlayerLoader] Character.wz failed: " + e.Message); return; }

            var img = ch[bodyImg] as WzImage;
            if (img == null) { Debug.LogError("[PlayerLoader] image not found: " + bodyImg); return; }
            img.ParseImage();

            var frame = img[animState]?["0"]?["body"] as WzCanvasProperty;
            if (frame == null) { Debug.LogError("[PlayerLoader] frame missing: " + animState + "/0/body"); return; }

            var bmp = frame.GetBitmap();
            if (bmp == null) { Debug.LogError("[PlayerLoader] bitmap is null"); return; }

            var bytes = Util.ImageToByteArray(bmp);
            sr.sprite = JCS_ImageLoader.Create(bytes);
        }
    }
}
