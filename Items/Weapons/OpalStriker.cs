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
	public class OpalStriker : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/OpalStriker");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Opal Striker");
			Item.damage = 10;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 64;
			Item.height = 16;
			////Tooltip.SetDefault("Fires a string of opal strikes");
			Item.useTime = 5;
			Item.reuseDelay = 25;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 1f;
			Item.value = 90000;
			Item.rare = 3;
			Item.UseSound = new Terraria.Audio.SoundStyle("CalamityModClassic1Point1/Sounds/Item/OpalStrike");
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("OpalStrike").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 6f;
			Item.useAmmo = 97;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("OpalStrike").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Marble, 20);
            recipe.AddIngredient(ItemID.Amber, 5);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddIngredient(ItemID.FlintlockPistol);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
		}
	}
}