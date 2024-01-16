using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class BarracudaGun : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/BarracudaGun");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Barracuda Gun");
        Item.damage = 70;
        Item.channel = true;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 54;
        Item.height = 28;
        Item.scale = 1.1f;
        ////Tooltip.SetDefault("Chew through them all!");
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 1.05f;
        Item.value = 900000;
        Item.rare = 9;
        Item.UseSound = SoundID.Item10;
        Item.autoReuse = true;
        Item.shootSpeed = 15f;
        Item.shoot = Mod.Find<ModProjectile>("MechanicalBarracuda").Type;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(-10, 0);
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
		float SpeedA = velocity.X;
   		float SpeedB = velocity.Y;
        int num6 = Main.rand.Next(2, 3);
        for (int index = 0; index < num6; ++index)
        {
      	 	float num7 = velocity.X;
            float num8 = velocity.Y;
            float SpeedX = velocity.X + (float) Main.rand.Next(-10, 11) * 0.05f;
            float SpeedY = velocity.Y + (float) Main.rand.Next(-10, 11) * 0.05f;
            Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY),type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
        }
        return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.PiranhaGun);
        recipe.AddIngredient(null, "CoreofCalamity", 2);
        recipe.AddIngredient(ItemID.SharkFin, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}