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

namespace MSU
{
    /// MVP movement: WASD / arrow keys move the GameObject in world space.
    /// Horizontal input also flips the sprite so the character faces movement direction.
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5.0f;

        private SpriteRenderer sr;

        private void Awake() { sr = GetComponent<SpriteRenderer>(); }

        private void Update()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            var delta = new Vector3(h, v, 0f) * speed * Time.deltaTime;
            transform.position += delta;

            if (sr != null && h != 0f) sr.flipX = h < 0f;
        }
    }
}
