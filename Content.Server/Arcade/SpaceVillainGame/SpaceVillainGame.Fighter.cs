// SPDX-FileCopyrightText: 2023 TemporalOroboros <TemporalOroboros@gmail.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

namespace Content.Server.Arcade.SpaceVillain;

public sealed partial class SpaceVillainGame
{
    /// <summary>
    /// A state holder for the fighters in the SpaceVillain game.
    /// </summary>
    public sealed class Fighter
    {
        private int _hp;
        private int _hpMax;
        private int _mp;
        private int _mpMax;

        /// <summary>
        /// Whether the given fighter can take damage/lose mana.
        /// </summary>
        [ViewVariables(VVAccess.ReadWrite)]
        public bool Invincible = false;

        /// <summary>
        /// The current hit point total of the fighter.
        /// </summary>
        [ViewVariables(VVAccess.ReadWrite)]
        public int Hp
        {
            get => _hp;
            set => _hp = MathHelper.Clamp(value, 0, HpMax);
        }

        /// <summary>
        /// The maximum hit point total of the fighter.
        /// </summary>
        [ViewVariables(VVAccess.ReadWrite)]
        public int HpMax
        {
            get => _hpMax;
            set
            {
                _hpMax = Math.Max(value, 0);
                Hp = MathHelper.Clamp(Hp, 0, HpMax);
            }
        }

        /// <summary>
        /// The current mana total of the fighter.
        /// </summary>
        [ViewVariables(VVAccess.ReadWrite)]
        public int Mp
        {
            get => _mp;
            set => _mp = MathHelper.Clamp(value, 0, MpMax);
        }

        /// <summary>
        /// The maximum mana total of the fighter.
        /// </summary>
        [ViewVariables(VVAccess.ReadWrite)]
        public int MpMax
        {
            get => _mpMax;
            set
            {
                _mpMax = Math.Max(value, 0);
                Mp = MathHelper.Clamp(Mp, 0, MpMax);
            }
        }
    }
}
