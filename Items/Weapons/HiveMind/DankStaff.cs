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

namespace CalamityModClassicPreTrailer.Items.Weapons.HiveMind
{
	public class DankStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dank Staff");
			// Tooltip.SetDefault("Summons a dank creeper to fight for you");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 7;
	        Item.mana = 10;
	        Item.width = 58;
	        Item.height = 58;
	        Item.useTime = 36;
	        Item.useAnimation = 36;
	        Item.scale = 0.85f;
	        Item.useStyle = 1;
	        Item.noMelee = true;
	        Item.knockBack = 2.25f;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
	        Item.UseSound = SoundID.Item44;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("DankCreeper").Type;
	        Item.shootSpeed = 10f;
	        Item.DamageType = DamageClass.Summon;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.RottenChunk, 3);
	        recipe.AddIngredient(ItemID.DemoniteBar, 8);
	        recipe.AddIngredient(null, "TrueShadowScale", 7);
	        recipe.AddTile(TileID.DemonAltar);
	        recipe.Register();
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