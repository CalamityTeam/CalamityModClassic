using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons
{
	public class MagnaStriker : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/MagnaStriker");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Magna Striker");
			Item.damage = 43;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 60;
			Item.height = 22;
			////Tooltip.SetDefault("Fires a string of opal and magna strikes");
			Item.useTime = 5;
			Item.reuseDelay = 25;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 2.25f;
			Item.value = 900000;
			Item.rare = 8;
			Item.UseSound = new Terraria.Audio.SoundStyle("CalamityModClassic1Point1/Sounds/Item/OpalStrike");
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("OpalStrike").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 12f;
			Item.useAmmo = 97;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int randomProj = Main.rand.Next(2);
			if (randomProj == 0)
			{
				Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("OpalStrike").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			}
			else
			{
				Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("MagnaStrike").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			}
		    return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "OpalStriker");
            recipe.AddIngredient(null, "MagnaCannon");
            recipe.AddIngredient(ItemID.AdamantiteBar, 6);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "OpalStriker");
            recipe.AddIngredient(null, "MagnaCannon");
            recipe.AddIngredient(ItemID.TitaniumBar, 6);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
		}
	}
}