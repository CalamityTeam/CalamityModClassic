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
	public class Megafleet : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/Megafleet");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Megafleet");
			Item.damage = 630;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 96;
			Item.height = 38;
			////Tooltip.SetDefault("Consumes fallen stars; 95% chance to not consume stars\nBLOWS UP EVERYTHING AT WARP SPEED!!!");
			Item.useTime = 7;
			Item.useAnimation = 7;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 15f;
			Item.value = 10000000;
			Item.expert = true;
			Item.UseSound = SoundID.Item92;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("PlasmaBlast").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 15f;
			Item.useAmmo = 75;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 95)
	    		return false;
	    	return true;
	    }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
		    float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
		    Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY),type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Megalodon");
            recipe.AddIngredient(null, "Starfleet");
            recipe.AddIngredient(null, "ShadowspecBar", 5);
            recipe.AddIngredient(ItemID.SoulofMight, 30);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
		}
	}
}