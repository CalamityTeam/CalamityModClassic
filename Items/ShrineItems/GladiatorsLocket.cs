using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.ShrineItems
{
	public class GladiatorsLocket : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gladiator's Locket");
			// Tooltip.SetDefault("Summons two spirit swords to protect you");
		}

	    public override void SetDefaults()
	    {
	        Item.width = 42;
	        Item.height = 36;
	        Item.value = Item.buyPrice(0, 9, 0, 0);
	        Item.rare = 3;
			Item.defense = 5;
	        Item.accessory = true;
	    }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>(); //there might be an upgrade sometime later?
			if (modPlayer.gladiatorSword)
            {
                return false;
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
	    	CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.gladiatorSword = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(Mod.Find<ModBuff>("GladiatorSwords").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("GladiatorSwords").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("GladiatorSword").Type] < 1)
				{
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("GladiatorSword").Type, (int)(20f * player.GetDamage(DamageClass.Summon).Multiplicative), 6f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("GladiatorSword2").Type, (int)(20f * player.GetDamage(DamageClass.Summon).Multiplicative), 6f, Main.myPlayer, 0f, 0f);
				}
			}
		}
	}
}