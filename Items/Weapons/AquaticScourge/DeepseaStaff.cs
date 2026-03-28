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

namespace CalamityModClassicPreTrailer.Items.Weapons.AquaticScourge
{
	public class DeepseaStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Deepsea Staff");
			// Tooltip.SetDefault("Summons an aquatic star to fight for you");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 48;
	        Item.mana = 10;
	        Item.width = 46;
	        Item.height = 46;
	        Item.useTime = 36;
	        Item.useAnimation = 36;
	        Item.useStyle = 1;
	        Item.noMelee = true;
	        Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
	        Item.UseSound = SoundID.Item44;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("AquaticStar").Type;
	        Item.shootSpeed = 10f;
	        Item.DamageType = DamageClass.Summon;
	    }

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse != 2)
			{
				position = Main.MouseWorld;
				velocity.X = 0;
				velocity.Y = 0;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
			}
			return false;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim(true);
			}
			return base.UseItem(player);
		}
	}
}