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

namespace CalamityModClassicPreTrailer.Items.Weapons.GreatSandShark
{
	public class SandSharknadoStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sand Sharknado Staff");
			// Tooltip.SetDefault("Summons a sandnado to fight for you");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 98;
	        Item.mana = 10;
	        Item.width = 48;
	        Item.height = 56;
	        Item.useTime = 35;
	        Item.useAnimation = 35;
	        Item.useStyle = 1;
	        Item.noMelee = true;
	        Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
	        Item.UseSound = SoundID.Item44;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("Sandnado").Type;
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

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TempestStaff);
            recipe.AddIngredient(null, "GrandScale");
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.AncientCloth, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}